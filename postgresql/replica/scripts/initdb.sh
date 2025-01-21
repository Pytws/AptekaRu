#!/bin/bash

if [ "$(ls -A $PGDATA)" ]; then
  postgres -c listen_addresses='localhost, 10.10.1.2' -c hba_file=/etc/postgresql/pg_hba.conf
else
  until pg_basebackup --pgdata=/var/lib/postgresql/data -R --slot=replication_slot --host=pg_master --port=5432
  do
  echo 'Waiting for primary to connect...'
  sleep 1s
  done
  echo 'Backup done, starting replica...'
  chmod 0700 /var/lib/postgresql/data
  postgres -c listen_addresses='localhost, 10.10.1.2' -c hba_file=/etc/postgresql/pg_hba.conf
fi
