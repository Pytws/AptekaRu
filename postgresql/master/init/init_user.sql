CREATE USER replicator WITH REPLICATION ENCRYPTED PASSWORD 'password';
SELECT pg_create_physical_replication_slot('replication_slot');

CREATE ROLE mon_user WITH LOGIN ENCRYPTED PASSWORD 'password';
GRANT pg_read_all_stats TO mon_user;


