using Microsoft.AspNetCore.Mvc;
using AptekaRu.DAL.Interfaces;
using AptekaRu.DAL.Enums;
using AptekaRu.DAL.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using System.Text.Json;
using AptekaRu.Web.Models;
using Npgsql;
using AptekaRu.Web.CustomJsonConverter;

namespace AptekaRu.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly IRenderTable renderTable;
        private readonly IAptekaruRepository aptekaruRepository;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly List<string> schemaPermitted;
        private readonly Dictionary<string, Type> modelpermitted;
        public DataController(IRenderTable renderTable, IAptekaruRepository aptekaruRepository)
        {
            this.aptekaruRepository = aptekaruRepository;
            this.renderTable = renderTable;
            jsonSerializerOptions = new()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            jsonSerializerOptions.Converters.Add(new CustomBooleanJsonConverter());
            modelpermitted = new Dictionary<string, Type>
            {
                { "client", typeof(Client) },
                { "drugs", typeof(Drug) },
                { "employees", typeof(Employee) },
                { "images", typeof(Image) },
                { "items_purchases", typeof(ItemsPurchase) },
                { "job_titles", typeof(JobTitle) },
                { "pharmacies", typeof(Pharmacy) },
                { "purchases", typeof(Purchase) },
                { "shedules", typeof(Shedule) },
                { "supplies", typeof(Supply) },
                { "type_drags", typeof(TypeDrag) },
                { "work_shedules", typeof(WorkShedule) }
            };
            schemaPermitted = ["aptekaru"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTables(string schemaName)
        {
            if (schemaName is null || !schemaName.Contains(schemaName)) return View("_MessageV1Partial", "Not a valid scheme");

            ViewBag.schemaName = schemaName;

            var tableInfo = await renderTable.GetTablesInformation(schemaName, TableType.BaseTable);

            return View(tableInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Create(
            [BindRequired] string schemaName,
            [BindRequired] string tableName)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName) ||
                !modelpermitted.ContainsKey(tableName))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            ViewBag.schemaName = schemaName;
            ViewBag.tableName = tableName;

            return View("Create", await renderTable.GetColumnsInformation(schemaName, TableType.BaseTable, tableName));
        }

        [HttpPost]
        public async Task<IActionResult> CreateIs(
            [FromForm] Dictionary<string, string> tableData,
            [FromForm][BindRequired] string schemaName,
            [FromForm][BindRequired] string tableName)
        {
            if (!modelpermitted.TryGetValue(tableName, out var value) ||
                !ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName))
            {
                return BadRequest("Not valid schema name or table name");
            }

            var json = JsonSerializer.Serialize(tableData);

            try
            {

                var obj = JsonSerializer.Deserialize(json, value, jsonSerializerOptions)!;

                await aptekaruRepository.Create(obj, schemaName, tableName);
                return View("_MessageV1Partial", "Entity created");

            }
            catch (JsonException)
            {
                return View("_MessageV1Partial", "Invalid data type");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Read(
            [FromQuery][BindRequired] string schemaName,
            [FromQuery][BindRequired] string tableName,
            [FromQuery] int offset = 0)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName) ||
                !modelpermitted.TryGetValue(tableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            if (offset < 0) return View("_MessageV1Partial", "Not valid offset");

            ViewBag.offset = offset;
            ViewBag.tableName = tableName;
            ViewBag.schemaName = schemaName;

            var rows = await aptekaruRepository.Read(schemaName, tableName, 10, offset);

            return View(rows);
        }

        [HttpPost]
        public async Task<IActionResult> ReadIs([FromBody] OffsetViewModel offsetView)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(offsetView.SchemaName) ||
                !modelpermitted.TryGetValue(offsetView.TableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            if (offsetView.Offset < 0) return BadRequest("Not valid offset");

            offsetView.Rows = await aptekaruRepository.Read(
                offsetView.SchemaName, offsetView.TableName, 10, offsetView.Offset);

            return Json(offsetView.Rows);
        }

        [HttpGet]
        public async Task<IActionResult> GetConstraints(
            [FromQuery][BindRequired] string schemaName,
            [FromQuery][BindRequired] string tableName,
            [FromQuery][BindRequired] string operation)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName) ||
                !modelpermitted.TryGetValue(tableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            if (operation != "update" && operation != "delete")
            {
                return View("_MessageV1Partial", $"Not valid operation: {operation}");
            }

            ViewBag.schemaName = schemaName;
            ViewBag.tableName = tableName;
            ViewBag.operation = operation;

            var constraints = await renderTable.GetConstraintInfo(schemaName, tableName, ConstraintType.PrimaryKey);

            return View(constraints);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateIs(
            [FromQuery][BindRequired] string schemaName,
            [FromQuery][BindRequired] string tableName,
            [FromQuery][BindRequired] string identifier)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName) ||
                !modelpermitted.TryGetValue(tableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            var constraintInfo = await renderTable.GetConstraintInfo(schemaName, tableName, ConstraintType.PrimaryKey);
            var columnsInfo = await renderTable.GetColumnsInformation(schemaName, TableType.BaseTable, tableName);
            var columnName = constraintInfo.ElementAt(0).ColumnName;

            ViewBag.schemaName = schemaName;
            ViewBag.tableName = tableName;
            ViewBag.identifier = identifier;

            if (Guid.TryParse(identifier, out var guidTypeIdentifier) && 
                constraintInfo.ElementAt(0).DataType == "uuid")
            {
                var entity = await aptekaruRepository.GetByGuid(schemaName, tableName, guidTypeIdentifier, columnName);
                return View(new TableModel(columnsInfo, entity));
            }
            else if (int.TryParse(identifier, out var intTypeIdentifier) &&
                constraintInfo.ElementAt(0).DataType == "integer")
            {
                var entity = await aptekaruRepository.GetById(schemaName, tableName, intTypeIdentifier, columnName);
                return View(new TableModel(columnsInfo, entity));
            }
            else
            {
                return View("_MessageV1Partial", "Not valid type identifier");
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateIs(
            [FromForm] Dictionary<string, string> tableData,
            [FromForm][BindRequired] string schemaName,
            [FromForm][BindRequired] string tableName,
            [FromForm][BindRequired] string identifier)
        {
            if (!ModelState.IsValid ||
                !schemaPermitted.Contains(schemaName) ||
                !modelpermitted.TryGetValue(tableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            var json = JsonSerializer.Serialize(tableData);
            var constraintInfo = await renderTable.GetConstraintInfo(schemaName, tableName, ConstraintType.PrimaryKey);

            try
            {
                var isInt = int.TryParse(identifier, out var intId);
                var isGuid = Guid.TryParse(identifier, out var guidId);

                if (!isGuid && !isInt) return View("_MessageV1Partial", "identifier must be id or guid");

                var obj = JsonSerializer.Deserialize(json, typeModel, jsonSerializerOptions);

                await aptekaruRepository.Update(obj, schemaName, tableName, constraintInfo.ElementAt(0), identifier);
                return View("_MessageV1Partial", $"Data updated by id: {identifier}");

            }
            catch (JsonException)
            {
                return View("_MessageV1Partial", "Invalid data type");
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                return View("_MessageV1Partial", ex.Detail);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIs(
            [FromForm][BindRequired] string schemaName,
            [FromForm][BindRequired] string tableName,
            [FromForm][BindRequired] string identifier)
        {
            if (!ModelState.IsValid ||
               !schemaPermitted.Contains(schemaName) ||
               !modelpermitted.TryGetValue(tableName, out var typeModel))
            {
                return View("_MessageV1Partial", "Not valid schema name or table name");
            }

            var isInt = int.TryParse(identifier, out var intId);
            var isGuid = Guid.TryParse(identifier, out var guidId);

            if (!isGuid && !isInt) return View("_MessageV1Partial", "identifier must be integer or guid");

            var constraintInfo = await renderTable.GetConstraintInfo(schemaName, tableName, ConstraintType.PrimaryKey);

            await aptekaruRepository.Delete(schemaName, tableName, constraintInfo.ElementAt(0), identifier);

            return View("_MessageV1Partial", $"Entity removed by id: {identifier}");
        }
    }
}
