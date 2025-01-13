"use strict";

function generateHtmlRows(rows) {
    const tbody = document.getElementById("data-content");
    const tr = tbody.querySelectorAll("tr");
    tr.forEach(tr => {
        tr.remove();
    });

    for (let row of rows) {
        let tr = document.createElement("tr");
        tr.className = "input-row";

        for (let key in row) {
            let td = document.createElement("td");
            td.className = "input-column";
            td.textContent = row[key];
            tr.appendChild(td);
            tbody.appendChild(tr);
        }
    }
}

$(document).ready(function () {
    let offsetModel = {
        offset: 0,
        schemaName: document.getElementById("schemaName").value,
        tableName: document.getElementById("tableName").value,
    };

    $("#NextButtom").click(function () {
        if (offsetModel.offset >= 0 && offsetModel.rows != 0) {
            offsetModel.offset += 10;
            $.ajax({
                type: "POST",
                url: "/Data/ReadIs",
                contentType: "application/json",
                data: JSON.stringify({
                    offset: offsetModel.offset,
                    tableName: offsetModel.tableName,
                    schemaName: offsetModel.schemaName
                }),
                success: function (result) {
                    offsetModel.rows = result;
                    generateHtmlRows(offsetModel.rows);
                },
                error: function (req, status, error) {
                    console.error(status);
                }
            });
        }
    });

    $("#PreviousButton").click(function () {
        if (offsetModel.offset >= 10) {
            offsetModel.offset -= 10;

            $.ajax({
                type: "POST",
                url: "/Data/ReadIs",
                contentType: "application/json",
                data: JSON.stringify({
                    offset: offsetModel.offset,
                    tableName: offsetModel.tableName,
                    schemaName: offsetModel.schemaName
                }),
                success: function (result) {
                    offsetModel.rows = result;
                    generateHtmlRows(offsetModel.rows);
                },
                error: function (req, status, error) {
                    console.error(status);
                }
            });
        }
    });
});