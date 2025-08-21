# Oracle Database Administration Notes

This document provides a comprehensive guide to common Oracle Database administration tasks, including connecting to the database, checking versions, managing listeners, tablespaces, data files, and user management.

## Connecting to Oracle Database

To connect as the system administrator:

```sql
sys as sysdba
```

## Checking Database Version

To view the database version:

```sql
SELECT * FROM v$version;
```

## Starting and Stopping the Database

To shut down the database:

```sql
shutdown
```

To start the database:

```sql
startup
```

## User and Database Information

To display the current user:

```sql
show user
```

To display the database name:

```sql
SELECT name FROM v$database;
```

## Managing Pluggable Databases (PDBs)

To show pluggable databases:

```sql
SHOW PDBS;
```

To open a pluggable database:

```sql
ALTER PLUGGABLE DATABASE ORCLPDB OPEN;
```

To switch session to a specific PDB:

```sql
ALTER SESSION SET CONTAINER=ORCLPDB;
```

## Managing Listeners

To start the listener:

```sql
lsnrctl start
```

To stop the listener:

```sql
lsnrctl stop
```

To create a new listener:

```bash
netca
```

To start a new listener (replace LISTENER_NAME with the actual listener name):

```sql
lsnrctl start LISTENER_NAME
```

## Managing Tablespaces and Data Files

### Viewing Tablespaces

To list all tablespaces:

```sql
SELECT * FROM dba_tablespaces;
```

To list only tablespace names:

```sql
SELECT tablespace_name FROM dba_tablespaces;
```

### Viewing Data Files

To get the size of data files (in MB):

```sql
SELECT tablespace_name, file_name, bytes/1024/1024 AS size_mb FROM dba_data_files;
```

### Creating a New Tablespace

To create a new tablespace (example with `iamfake`):

```sql
CREATE TABLESPACE iamfake
DATAFILE 'F:\db_home\oradata\ORCL\iamfake.dbf'
SIZE 10M
AUTOEXTEND ON
NEXT 10M
MAXSIZE 50M;
```

**Note**: The `SIZE 10M` specifies the initial size of the tablespace.

### Setting a Default Tablespace

To set a new tablespace as the default:

```sql
ALTER DATABASE DEFAULT TABLESPACE iamfake;
```

To revert to the SYSTEM tablespace as the default:

```sql
ALTER DATABASE DEFAULT TABLESPACE SYSTEM;
```

### Deleting a Tablespace

To delete a tablespace and its associated data files:

```sql
DROP TABLESPACE iamfake INCLUDING CONTENTS AND DATAFILES;
```

### Viewing Default Tablespace for Users

To check the default tablespace for a user:

```sql
SELECT username, default_tablespace FROM dba_users;
```

## User Management

### Viewing Users

To list all users:

```sql
SELECT * FROM dba_users;
```

To list only usernames:

```sql
SELECT username FROM dba_users;
```

### Creating a User in a Pluggable Database

1. Switch to the pluggable database:

```sql
ALTER SESSION SET CONTAINER=ORCLPDB;
```

2. Create a user:

```sql
CREATE USER iamusera IDENTIFIED BY test123;
```

3. Grant permissions:

```sql
GRANT CONNECT, RESOURCE TO iamusera;
```

4. Verify user creation:

```sql
SELECT username FROM dba_users WHERE username = 'iamusera';
```

### Creating a Common User (Non-PDB)

To create a common user (prefix with `C##`):

```sql
CREATE USER C##iamfakeuser IDENTIFIED BY test123;
```

### Granting Privileges to a Common User

1. Connect as `sysdba`:

```sql
CONNECT sys as sysdba
```

2. Grant session privilege:

```sql
GRANT CREATE SESSION TO C##iamfakeuser;
```

3. Grant additional privileges (e.g., create table, create view):

```sql
GRANT CREATE TABLE TO C##iamfakeuser;
GRANT CREATE VIEW TO C##iamfakeuser;
```

4. Set unlimited quota on the USERS tablespace:

```sql
ALTER USER C##iamfakeuser QUOTA UNLIMITED ON USERS;
```

### Example: Creating and Using a Table

1. Connect as the common user:

```sql
CONNECT C##iamfakeuser;
```

2. Create a table (example: `student`):

```sql
CREATE TABLE student (
    id NUMBER PRIMARY KEY,
    name VARCHAR2(50),
    age NUMBER
);
```

3. Insert data into the table:

```sql
INSERT INTO student VALUES (1, 'Ram', 20);
COMMIT;
```

4. Query the table:

```sql
SELECT * FROM student;
```

**Output**:

```
ID  NAME  AGE
1   Ram   20
```

## Troubleshooting Common Errors

- **ORA-01005: null password given; logon denied**  
  Ensure the correct password is provided when connecting.

- **ORA-01045: user lacks CREATE SESSION privilege; logon denied**  
  Grant the `CREATE SESSION` privilege to the user:

```sql
GRANT CREATE SESSION TO C##iamfakeuser;
```

- **ORA-00904: invalid identifier**  
  Check for syntax errors in SQL statements, such as missing commas or incorrect column definitions.

- **SP2-0640: Not connected**  
  Ensure you are connected to the database (e.g., `CONNECT sys as sysdba`) before running commands.

- **SP2-0158: unknown SHOW option**  
  Verify the correct command (e.g., `show user` instead of `show usewrl` or `show table`).

# Oracle Database 19c: Undo Tablespace Management Practice

This README documents the practice session conducted on August 15, 2025, at college, focusing on managing undo tablespaces in Oracle Database 19c Enterprise Edition (Version 19.3.0.0.0). The session involved logging into an Oracle database instance running in a Docker container, performing various SQL commands related to undo tablespace management, and debugging errors encountered during the process. The full command output is included below for reference.

## Session Overview

The practice involved connecting to an Oracle 19c database instance using `sqlplus` as the `SYS` user with `SYSDBA` privileges. The primary focus was on querying and modifying undo tablespace settings, creating a new undo tablespace, switching between tablespaces, and handling errors. Below is a summary of the commands executed, their outputs, and the debugging process for errors encountered.

## Commands Executed and Outputs

1. **Access the Docker Container and SQL\*Plus**:

   ```bash
   sudo docker exec -it oracle19c-new bash
   bash-4.2$ sqlplus
   ```

   **Output**:

   ```
   SQL*Plus: Release 19.0.0.0.0 - Production on Fri Aug 15 02:25:19 2025
   Version 19.3.0.0.0

   Copyright (c) 1982, 2019, Oracle.  All rights reserved.

   Enter user-name: sys as sysdba
   Enter password:

   Connected to:
   Oracle Database 19c Enterprise Edition Release 19.0.0.0.0 - Production
   Version 19.3.0.0.0
   ```

2. **Check Current Undo Tablespace Settings**:

   ```sql
   show parameter undo_tablespace;
   ```

   **Output**:

   ```
   NAME                                 TYPE        VALUE
   ------------------------------------ ----------- ------------------------------
   undo_tablespace                      string      UNDOTBS1
   ```

3. **Check Undo Retention**:

   ```sql
   show parameter undo_retention;
   ```

   **Output**:

   ```
   NAME                                 TYPE        VALUE
   ------------------------------------ ----------- ------------------------------
   undo_retention                       integer     900
   ```

4. **Query Undo Tablespaces**:

   ```sql
   SELECT TABLESPACE_NAME, RETENTION FROM DBA_TABLESPACE WHERE CONTENTS = 'undo';
   ```

   **Output**:

   ```
   SELECT TABLESPACE_NAME, RETENTION FROM DBA_TABLESPACE
                                          *
   ERROR at line 1:
   ORA-00942: table or view does not exist
   ```

   ```sql
   SELECT TABLESPACE_NAME, RETENTION FROM DBA_TABLESPACES WHERE CONTENT = 'undo';
   ```

   **Output**:

   ```
   WHERE CONTENT = 'undo'
         *
   ERROR at line 3:
   ORA-00904: "CONTENT": invalid identifier
   ```

   ```sql
   SELECT TABLESPACE_NAME, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'undo';
   ```

   **Output**:

   ```
   no rows selected
   ```

   ```sql
   select tablesapce_name, retention FROM dba_tablespaces where contents = 'UNDO';
   ```

   **Output**:

   ```
   select tablesapce_name, retention
          *
   ERROR at line 1:
   ORA-00904: "TABLESAPCE_NAME": invalid identifier
   ```

   ```sql
   SELECT TABLESPACE_NAME, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'UNDO';
   ```

   **Output**:

   ```
   TABLESPACE_NAME                RETENTION
   ------------------------------ -----------
   UNDOTBS1                      NOGUARANTEE
   ```

5. **Check User**:

   ```sql
   show user;
   ```

   **Output**:

   ```
   USER is "SYS"
   ```

6. **Create a New Undo Tablespace**:

   ```sql
   CREATE UNDO_TABLESPACE undotbs2 DATAFILE 'undotbs2.dbf' SIZE 50M AUTOEXTEND ON;
   ```

   **Output**:

   ```
   CREATE UNDO_TABLESPACE undotbs2
          *
   ERROR at line 1:
   ORA-00901: invalid CREATE command
   ```

   ```sql
   CREATE UNDO TABLESPACE undotbs2 DATAFILE 'undotbs2.dbf' SIZE 50M AUTOEXTEND ON;
   ```

   **Output**:

   ```
   Tablespace created.
   ```

7. **Switch to New Undo Tablespace**:

   ```sql
   ALTER SYSTEM SET undo_tablespace = undotbs2;
   ```

   **Output**:

   ```
   System altered.
   ```

8. **Verify Undo Tablespace Change**:

   ```sql
   show parameter undo_tablespace;
   ```

   **Output**:

   ```
   NAME                                 TYPE        VALUE
   ------------------------------------ ----------- ------------------------------
   undo_tablespace                      string      UNDOTBS2
   ```

9. **Query Undo Tablespaces Again**:

   ```sql
   SELECT TABLE_SPACE, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'UNDO';
   ```

   **Output**:

   ```
   SELECT TABLE_SPACE, RETENTION
          *
   ERROR at line 1:
   ORA-00904: "TABLE_SPACE": invalid identifier
   ```

   ```sql
   SELECT TABLESPACE_NAME, CONTENTS, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'UNDO';
   ```

   **Output**:

   ```
   TABLESPACE_NAME                CONTENTS              RETENTION
   ------------------------------ --------------------- -----------
   UNDOTBS1                      UNDO                  NOGUARANTEE
   UNDOTBS2                      UNDO                  NOGUARANTEE
   ```

10. **Set Retention Guarantee**:

    ```sql
    ALTER TABLESPACE undotbs2 RETENTION GUARANTEE;
    ```

    **Output**:

    ```
    ALTER TABLESPACE undotbs2 RETENTION GUARANE
                                        *
    ERROR at line 1:
    ORA-02142: missing or invalid ALTER TABLESPACE option
    ```

    ```sql
    ALTER TABLESPACE undotbs2 RETENTION GUARANTEE;
    ```

    **Output**:

    ```
    Tablespace altered.
    ```

11. **Verify Retention Change**:

    ```sql
    SELECT TABLESPACE_NAME, CONTENTS, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'undo';
    ```

    **Output**:

    ```
    no rows selected
    ```

    ```sql
    SELECT TABLESPACE_NAME, CONTENTS, RETENTION FROM DBA_TABLESPACES WHERE CONTENTS = 'UNDO';
    ```

    **Output**:

    ```
    TABLESPACE_NAME                CONTENTS              RETENTION
    ------------------------------ --------------------- -----------
    UNDOTBS1                      UNDO                  NOGUARANTEE
    UNDOTBS2                      UNDO                  GUARANTEE
    ```

12. **Switch Back to Original Tablespace and Drop New Tablespace**:

    ```sql
    ALTER SYSTEM SET undo_tablespace = undotbs1;
    ```

    **Output**:

    ```
    System altered.
    ```

    ```sql
    DROP TABLESPACE undotbs2 INCLUDING CONTENTS AND DATAFILES;
    ```

    **Output**:

    ```
    Tablespace dropped.
    ```

## Debugging Errors

Below are the errors encountered during the session, their causes, and how they were resolved:

1. **ORA-00942: table or view does not exist**:

   - **Cause**: Queried `DBA_TABLESPACE` (singular) instead of `DBA_TABLESPACES` (plural).
   - **Resolution**: Corrected the table name to `DBA_TABLESPACES`.

2. **ORA-00904: "CONTENT": invalid identifier**:

   - **Cause**: Used incorrect column name `CONTENT` instead of `CONTENTS`.
   - **Resolution**: Changed to `CONTENTS` in the query.

3. **ORA-00904: "TABLESAPCE_NAME": invalid identifier**:

   - **Cause**: Typo in `TABLESPACE_NAME` (`TABLESAPCE_NAME`).
   - **Resolution**: Corrected the spelling to `TABLESPACE_NAME`.

4. **ORA-00901: invalid CREATE command**:

   - **Cause**: Incorrect syntax `CREATE UNDO_TABLESPACE` instead of `CREATE UNDO TABLESPACE`.
   - **Resolution**: Added a space between `UNDO` and `TABLESPACE`.

5. **ORA-02142: missing or invalid ALTER TABLESPACE option**:

   - **Cause**: Typo in `GUARANTEE` (`GUARANE`).
   - **Resolution**: Corrected the spelling to `GUARANTEE`.

6. **No rows selected in query**:

   - **Cause**: Case sensitivity in the `WHERE CONTENTS = 'undo'` condition; Oracle expects `'UNDO'` (uppercase).
   - **Resolution**: Changed the condition to `WHERE CONTENTS = 'UNDO'`.

7. **SP2-0158: unknown SHOW option "paramter"**:
   - **Cause**: Typo in `parameter` (`paramter`) and incorrect tablespace name `undotbs1`.
   - **Resolution**: Corrected to `show parameter undo_tablespace`.

## Key Learnings

- **Case Sensitivity**: Oracle SQL is case-sensitive for string literals in conditions (e.g., `'UNDO'` vs. `'undo'`).
- **Syntax Accuracy**: Correct syntax is critical (e.g., `UNDO TABLESPACE` vs. `UNDO_TABLESPACE`, `TABLESPACE_NAME` vs. `TABLESAPCE_NAME`).
- **Retention Guarantee**: Setting `RETENTION GUARANTEE` ensures undo data is preserved for the retention period, useful for flashback operations, but may cause DML operations to fail if space is insufficient.
- **Automatic Undo Management**: Oracle 19c uses automatic undo management by default, simplifying undo space management compared to manual rollback segments.
- **Switching and Dropping Tablespaces**: The `ALTER SYSTEM SET undo_tablespace` command switches the active undo tablespace, and `DROP TABLESPACE` removes unused tablespaces, including their datafiles.

## Environment

- **Database**: Oracle Database 19c Enterprise Edition (Version 19.3.0.0.0)
- **Container**: Docker container (`oracle19c-new`)
- **Tool**: `sqlplus` as `SYS` user with `SYSDBA` privileges
- **Date and Time**: August 15, 2025, 08:38 AM +0545

## Recommendations for Future Practice

1. **Double-Check Syntax**: Use Oracle documentation or `DESCRIBE` commands to verify table and column names (e.g., `DESCRIBE DBA_TABLESPACES`).
2. **Monitor Undo Usage**: Use views like `V$UNDOSTAT` or `DBA_UNDO_EXTENTS` to monitor undo tablespace health and avoid errors like `ORA-30036` (unable to extend segment).
3. **Test in Non-Production**: Practice creating and dropping tablespaces in a test environment to avoid impacting production.
4. **Enable Autoextend**: Ensure undo tablespaces have `AUTOEXTEND ON` for dynamic growth, as demonstrated in the creation of `UNDOTBS2`.

## References

- Oracle Database 19c Documentation: [Managing Undo](https://docs.oracle.com/en/database/oracle/oracle-database/19/admin/managing-undo.html)
- Oracle Help Center: [UNDO_TABLESPACE](https://docs.oracle.com/en/database/oracle/oracle-database/19/refrn/UNDO_TABLESPACE.html)

## Backup

Below is a **README.md** file that documents the process of enabling archive log mode and performing a database backup using Oracle Recovery Manager (RMAN) for an Oracle 19c database running in a Docker container. The file includes the commands, their outputs, and explanations of each step.

# Oracle 19c Database Backup with RMAN in Docker

This README documents the steps to enable archive log mode and perform a full database backup using Oracle Recovery Manager (RMAN) for an Oracle 19c database running in a Docker container named `oracle19c-new`. The commands, their outputs, and explanations are provided to ensure clarity and reproducibility.

## Prerequisites

- Oracle 19c database installed in a Docker container (`oracle19c-new`).
- Docker is running, and the user has `sudo` privileges.
- Oracle SQL\*Plus and RMAN tools are available in the container.
- The database is accessible with `sys as sysdba` credentials.

## Steps to Enable Archive Log Mode and Perform Backup

### 1. Start the Docker Container

Start the Docker container to ensure the Oracle database is running.

```bash
sudo docker start oracle19c-new
```

**Output:**

```
oracle19c-new
```

**Explanation:**

- The `sudo docker start oracle19c-new` command starts the Docker container named `oracle19c-new` if it is not already running.
- The output confirms that the container has started successfully.

### 2. Access the Container

Enter the Docker container's bash shell to execute Oracle commands.

```bash
sudo docker exec -it oracle19c-new bash
```

**Output:**

- Opens an interactive bash session inside the container.

**Explanation:**

- The `sudo docker exec -it oracle19c-new bash` command starts an interactive terminal session (`-it`) inside the `oracle19c-new` container, allowing execution of commands like `sqlplus` and `rman`.

### 3. Connect to SQL\*Plus as SYSDBA

Launch SQL\*Plus and connect as the `sys` user with `sysdba` privileges.

```bash
sqlplus
```

**Prompt:**

```
Enter user-name: sys as sysdba
Enter password:
```

**Output:**

```
SQL*Plus: Release 19.0.0.0.0 - Production on Wed Aug 20 01:31:37 2025
Version 19.3.0.0.0

Copyright (c) 1982, 2019, Oracle.  All rights reserved.

Connected to:
Oracle Database 19c Enterprise Edition Release 19.0.0.0.0 - Production
Version 19.3.0.0.0
```

**Explanation:**

- `sqlplus` starts the SQL\*Plus client.
- Logging in as `sys as sysdba` grants administrative privileges to manage the database.
- The output confirms the connection to Oracle Database 19c Enterprise Edition.

### 4. Check Archive Log Mode

Verify the current archive log mode of the database.

```sql
archive log list
```

**Output:**

```
Database log mode           Archive Mode
Automatic archival         Enabled
Archive destination        /opt/oracle/product/19c/dbhome_1/dbs/arch
Oldest online log sequence 7
Next log sequence to archive 9
Current log sequence       9
```

**Explanation:**

- The `archive log list` command displays the database's archiving status.
- The output shows that the database is already in `Archive Mode` with automatic archiving enabled. The archive destination is `/opt/oracle/product/19c/dbhome_1/dbs/arch`.
- If the database is in `No Archive Mode`, it must be enabled (see Step 5).

### 5. Enable Archive Log Mode (if needed)

If the database is not in archive log mode, shut down the database, mount it, and enable archive log mode.

```sql
SHUTDOWN IMMEDIATE;
```

**Output:**

```
Database closed.
Database dismounted.
ORACLE instance shut down.
```

```sql
STARTUP MOUNT;
```

**Output:**

```
ORACLE instance started.

Total System Global Area 2432692416 bytes
Fixed Size               9138368 bytes
Variable Size            570425344 bytes
Database Buffers         1845493760 bytes
Redo Buffers             7634944 bytes
Database mounted.
```

```sql
ALTER DATABASE ARCHIVELOG;
```

**Output:**

```
Database altered.
```

```sql
archive log list
```

**Output:**

```
Database log mode           Archive Mode
Automatic archival         Enabled
Archive destination        /opt/oracle/product/19c/dbhome_1/dbs/arch
Oldest online log sequence 7
Next log sequence to archive 9
Current log sequence       9
```

**Explanation:**

- `SHUTDOWN IMMEDIATE` gracefully shuts down the database, closing and dismounting it.
- `STARTUP MOUNT` starts the database instance and mounts the database without opening it, which is required to change the archive log mode.
- `ALTER DATABASE ARCHIVELOG` enables archive log mode, allowing redo logs to be archived for point-in-time recovery.
- The final `archive log list` confirms that the database is now in `Archive Mode`.

**Note:** If you encounter errors like `SP2-0717: illegal SHUTDOWN option`, ensure the command is typed correctly (e.g., `IMMEDIATE`, not `IMMEDIATELY` or `IMEEDIATE`).

### 6. Exit SQL\*Plus

Exit SQL\*Plus to return to the bash shell.

```sql
exit;
```

**Output:**

```
Disconnected from Oracle Database 19c Enterprise Edition Release 19.0.0.0.0 - Production
Version 19.3.0.0.0
```

**Explanation:**

- The `exit` command disconnects from SQL\*Plus and returns to the container's bash shell.

### 7. Connect to RMAN

Start RMAN and connect to the target database.

```bash
rman target /
```

**Output:**

```
Recovery Manager: Release 19.0.0.0.0 - Production on Wed Aug 20 01:33:24 2025
Version 19.3.0.0.0

Copyright (c) 1982, 2019, Oracle and/or its affiliates.  All rights reserved.

connected to target database: ORCLCDB (DBID=2979897351, not open)
```

**Explanation:**

- `rman target /` starts RMAN and connects to the target database (`ORCLCDB`) using OS authentication.
- The output confirms the connection to the database with the specified DBID, which is in a mounted state (`not open`).

**Note:** If you encounter `RMAN: command not found`, ensure you are in the correct environment. The command `rman` is case-sensitive; try `rman` instead of `RMAN`.

### 8. List Existing Backups

Check for existing backups in the RMAN repository.

```rman
LIST BACKUP;
```

**Output:**

```
using target database control file instead of recovery catalog
specification does not match any backup in the repository
```

**Explanation:**

- The `LIST BACKUP` command displays all backups in the RMAN repository.
- The initial output indicates no backups exist yet.

### 9. Perform a Full Database Backup

Back up the database, including archived logs.

```rman
BACKUP DATABASE PLUS ARCHIVELOG;
```

**Output:**

```
Starting backup at 20-AUG-25
allocated channel: ORA_DISK_1
channel ORA_DISK_1: SID=622 device type=DISK
channel ORA_DISK_1: starting archived log backup set
channel ORA_DISK_1: specifying archived log(s) in backup set
input archived log thread=1 sequence=8 RECID=1 STAMP=1209605035
channel ORA_DISK_1: starting piece 1 at 20-AUG-25
channel ORA_DISK_1: finished piece 1 at 20-AUG-25
piece handle=/opt/oracle/product/19c/dbhome_1/dbs/0141i7hg_1_1 tag=TAG20250820T013440 comment=NONE
channel ORA_DISK_1: backup set complete, elapsed time: 00:00:03
Finished backup at 20-AUG-25

Starting backup at 20-AUG-25
using channel ORA_DISK_1
channel ORA_DISK_1: starting full datafile backup set
channel ORA_DISK_1: specifying datafile(s) in backup set
input datafile file number=00001 name=/opt/oracle/oradata/ORCLCDB/system01.dbf
input datafile file number=00003 name=/opt/oracle/oradata/ORCLCDB/sysaux01.dbf
input datafile file number=00004 name=/opt/oracle/oradata/ORCLCDB/undotbs01.dbf
input datafile file number=00013 name=/opt/oracle/oradata/ORCLCDB/ilovespace.dbf
input datafile file number=00007 name=/opt/oracle/oradata/ORCLCDB/users01.dbf
channel ORA_DISK_1: starting piece 1 at 20-AUG-25
channel ORA_DISK_1: finished piece 1 at 20-AUG-25
piece handle=/opt/oracle/product/19c/dbhome_1/dbs/0241i7hj_1_1 tag=TAG20250820T013443 comment=NONE
channel ORA_DISK_1: backup set complete, elapsed time: 00:00:15
channel ORA_DISK_1: starting full datafile backup set
channel ORA_DISK_1: specifying datafile(s) in backup set
input datafile file number=00010 name=/opt/oracle/oradata/ORCLCDB/ORCLPDB1/sysaux01.dbf
input datafile file number=00009 name=/opt/oracle/oradata/ORCLCDB/ORCLPDB1/system01.dbf
input datafile file number=00011 name=/opt/oracle/oradata/ORCLCDB/ORCLPDB1/undotbs01.dbf
input datafile file number=00012 name=/opt/oracle/oradata/ORCLCDB/ORCLPDB1/users01.dbf
channel ORA_DISK_1: starting piece 1 at 20-AUG-25
channel ORA_DISK_1: finished piece 1 at 20-AUG-25
piece handle=/opt/oracle/product/19c/dbhome_1/dbs/0341i7i3_1_1 tag=TAG20250820T013443 comment=NONE
channel ORA_DISK_1: backup set complete, elapsed time: 00:00:07
channel ORA_DISK_1: starting full datafile backup set
channel ORA_DISK_1: specifying datafile(s) in backup set
input datafile file number=00006 name=/opt/oracle/oradata/ORCLCDB/pdbseed/sysaux01.dbf
input datafile file number=00005 name=/opt/oracle/oradata/ORCLCDB/pdbseed/system01.dbf
input datafile file number=00008 name=/opt/oracle/oradata/ORCLCDB/pdbseed/undotbs01.dbf
channel ORA_DISK_1: starting piece 1 at 20-AUG-25
channel ORA_DISK_1: finished piece 1 at 20-AUG-25
piece handle=/opt/oracle/product/19c/dbhome_1/dbs/0441i7ia_1_1 tag=TAG20250820T013443 comment=NONE
channel ORA_DISK_1: backup set complete, elapsed time: 00:00:07
Finished backup at 20-AUG-25

Starting backup at 20-AUG-25
using channel ORA_DISK_1
specification does not match any archived log in the repository
backup cancelled because there are no files to backup
Finished backup at 20-AUG-25

Starting Control File and SPFILE Autobackup at 20-AUG-25
piece handle=/opt/oracle/product/19c/dbhome_1/dbs/c-2979897351-20250820-00 comment=NONE
Finished Control File and SPFILE Autobackup at 20-AUG-25
```

**Explanation:**

- The `BACKUP DATABASE PLUS ARCHIVELOG` command performs a full backup of the database and all archived redo logs.
- The command creates multiple backup sets:
  - **Backup Set 1**: Contains archived log sequence 8 (191.32 MB).
  - **Backup Set 2**: Contains datafiles for the CDB (1.22 GB).
  - **Backup Set 3**: Contains datafiles for the `ORCLPDB1` pluggable database (489.97 MB).
  - **Backup Set 4**: Contains datafiles for the `PDB$SEED` pluggable database (555.95 MB).
  - **Backup Set 5**: Contains the control file and SPFILE (17.95 MB).
- The backup pieces are stored in `/opt/oracle/product/19c/dbhome_1/dbs/`.
- An error occurred initially (`ARHIVELOG` misspelled as `ARCHIVELOG`), which was corrected in the subsequent command.
- The final attempt to back up archived logs was canceled because no new logs were available.

### 10. Verify the Backup

List the backups to confirm they were created successfully.

```rman
LIST BACKUP;
```

**Output:**

```
List of Backup Sets
===================

BS Key  Size       Device Type Elapsed Time Completion Time
------- ---------- ----------- ------------ ---------------
1       191.32M    DISK        00:00:01     20-AUG-25
        BP Key: 1   Status: AVAILABLE  Compressed: NO  Tag: TAG20250820T013440
        Piece Name: /opt/oracle/product/19c/dbhome_1/dbs/0141i7hg_1_1
  List of Archived Logs in backup set 1
  Thrd Seq     Low SCN    Low Time  Next SCN   Next Time
  ---- ------- ---------- --------- ---------- ---------
  1    8       2284316    15-AUG-25 2387814    20-AUG-25

BS Key  Type LV Size       Device Type Elapsed Time Completion Time
------- ---- -- ---------- ----------- ------------ ---------------
2       Full    1.22G      DISK        00:00:08     20-AUG-25
        BP Key: 2   Status: AVAILABLE  Compressed: NO  Tag: TAG20250820T013443
        Piece Name: /opt/oracle/product/19c/dbhome_1/dbs/0241i7hj_1_1
  List of Datafiles in backup set 2
  File LV Type Ckp SCN    Ckp Time  Abs Fuz SCN Sparse Name
  ---- -- ---- ---------- --------- ----------- ------ ----
  1       Full 2394960    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/system01.dbf
  3       Full 2394960    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/sysaux01.dbf
  4       Full 2394960    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/undotbs01.dbf
  7       Full 2394960    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/users01.dbf
  13      Full 2394960    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/ilovespace.dbf

BS Key  Type LV Size       Device Type Elapsed Time Completion Time
------- ---- -- ---------- ----------- ------------ ---------------
3       Full    489.97M    DISK        00:00:03     20-AUG-25
        BP Key: 3   Status: AVAILABLE  Compressed: NO  Tag: TAG20250820T013443
        Piece Name: /opt/oracle/product/19c/dbhome_1/dbs/0341i7i3_1_1
  List of Datafiles in backup set 3
  Container ID: 3, PDB Name: ORCLPDB1
  File LV Type Ckp SCN    Ckp Time  Abs Fuz SCN Sparse Name
  ---- -- ---- ---------- --------- ----------- ------ ----
  9       Full 2394018    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/ORCLPDB1/system01.dbf
  10      Full 2394018    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/ORCLPDB1/sysaux01.dbf
  11      Full 2394018    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/ORCLPDB1/undotbs01.dbf
  12      Full 2394018    20-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/ORCLPDB1/users01.dbf

BS Key  Type LV Size       Device Type Elapsed Time Completion Time
------- ---- -- ---------- ----------- ------------ ---------------
4       Full    555.95M    DISK        00:00:03     20-AUG-25
        BP Key: 4   Status: AVAILABLE  Compressed: NO  Tag: TAG20250820T013443
        Piece Name: /opt/oracle/product/19c/dbhome_1/dbs/0441i7ia_1_1
  List of Datafiles in backup set 4
  Container ID: 2, PDB Name: PDB$SEED
  File LV Type Ckp SCN    Ckp Time  Abs Fuz SCN Sparse Name
  ---- -- ---- ---------- --------- ----------- ------ ----
  5       Full 2156056    11-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/pdbseed/system01.dbf
  6       Full 2156056    11-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/pdbseed/sysaux01.dbf
  8       Full 2156056    11-AUG-25              NO    /opt/oracle/oradata/ORCLCDB/pdbseed/undotbs01.dbf

BS Key  Type LV Size       Device Type Elapsed Time Completion Time
------- ---- -- ---------- ----------- ------------ ---------------
5       Full    17.95M     DISK        00:00:00     20-AUG-25
        BP Key: 5   Status: AVAILABLE  Compressed: NO  Tag: TAG20250820T013513
        Piece Name: /opt/oracle/product/19c/dbhome_1/dbs/c-2979897351-20250820-00
  SPFILE Included: Modification time: 20-AUG-25
  SPFILE db_unique_name: ORCLCDB
  Control File Included: Ckp SCN: 2394960      Ckp time: 20-AUG-25
```

**Explanation:**

- The `LIST BACKUP` command confirms the creation of five backup sets, detailing their contents, sizes, and locations.
- Each backup set includes specific datafiles or archived logs, with timestamps and checkpoint SCNs for recovery purposes.
- The backups are stored on disk and are marked as `AVAILABLE` with no compression.

### 11. Exit RMAN

Exit the RMAN session.

```rman
exit;
```

**Output:**

```
Recovery Manager complete.
```

**Explanation:**

- The `exit` command closes the RMAN session and returns to the bash shell.

### 12. Exit the Container

Exit the Docker container's bash shell.

```bash
exit;
```

**Output:**

```
exit
```

**Explanation:**

- The `exit` command terminates the bash session inside the container, returning to the host machine's terminal.

## Common Errors and Fixes

- **SP2-0734: unknown command beginning "archive lo..."**
  - **Cause**: Misspelled command (`archive loglist` instead of `archive log list`).
  - **Fix**: Use the correct command: `archive log list`.
- **SP2-0717: illegal SHUTDOWN option**
  - **Cause**: Incorrect spelling of `IMMEDIATE` (e.g., `IMMEDIATELY` or `IMEEDIATE`).
  - **Fix**: Use `SHUTDOWN IMMEDIATE`.
- **RMAN-01009: syntax error: found "identifier": expecting one of: "archivelog"**
  - **Cause**: Misspelled `ARCHIVELOG` as `ARHIVELOG`.
  - **Fix**: Correct the spelling: `BACKUP DATABASE PLUS ARCHIVELOG`.
- **RMAN: command not found**
  - **Cause**: Case-sensitive command (`RMAN` instead of `rman`).
  - **Fix**: Use lowercase `rman`.

## Notes

- Ensure the database is in archive log mode before performing RMAN backups to enable point-in-time recovery.
- Backups are stored in `/opt/oracle/product/19c/dbhome_1/dbs/`. Ensure sufficient disk space in this directory.
- The database must be in `MOUNT` state to enable archive log mode but can be in `OPEN` or `MOUNT` state for RMAN backups.
- Regularly verify backups using `LIST BACKUP` to ensure they are available for recovery.

## References

- [Oracle Database Backup and Recovery User's Guide](https://docs.oracle.com/en/database/oracle/oracle-database/19/bradv/)
- [Oracle RMAN Commands Reference](https://docs.oracle.com/en/database/oracle/oracle-database/19/rcmrf/)

<xaiArtifact artifact_id="0934c17e-6a56-4981-8587-d0b34991cdcb" artifact_version_id="57eabdb1-55f7-49ce-86c6-daf3e9b0c26c" title="readme.md" contentType="text/markdown">

# Employee Database Management

This document provides an overview of the SQL operations performed on the `EMPLOYEE` table in an Oracle database, as executed by the user `C##TESTUSER`. The commands demonstrate basic database operations, including connecting to the database, querying, inserting, updating, and using flashback features to track changes.

## Prerequisites

- **Oracle Database**: Ensure you have access to an Oracle database instance.
- **User Credentials**: The user `C##TESTUSER` with appropriate permissions is required.
- **SQL\*Plus or Similar Tool**: Used to execute SQL commands.

## Database Operations

### 1. Connecting to the Database

The user connects to the database as `C##TESTUSER`. Initially, a login attempt fails due to an invalid password, but a subsequent attempt succeeds.

```sql
SQL> connect c##testuser;
Enter password:
Connected.
SQL> show user;
USER is "C##TESTUSER"
```

### 2. Table Structure

The `EMPLOYEE` table contains the following columns:

- `ID`: Numeric identifier for the employee.
- `NAME`: Employee's name (varchar).
- `AGE`: Employee's age (numeric).

List the tables owned by the user:

```sql
SQL> SELECT table_name FROM user_tables;
TABLE_NAME
----------
EMPLOYEE
```

### 3. Querying Data

Retrieve all records from the `EMPLOYEE` table:

```sql
SQL> SELECT * FROM employee;
   ID NAME       AGE
---- ---------- ---
    5 Abishek    22
    4 Rama       23
    1 Ram        25
```

Filter records based on conditions:

- Employees with age greater than 22:

```sql
SQL> SELECT * FROM employee WHERE age > 22;
   ID NAME       AGE
---- ---------- ---
    4 Rama       23
    1 Ram        25
```

- Employee with age equal to 22:

```sql
SQL> SELECT * FROM employee WHERE age = 22;
   ID NAME       AGE
---- ---------- ---
    5 Abishek    22
```

- Employee with name 'Abi':

```sql
SQL> SELECT * FROM employee WHERE name = 'Abi';
   ID NAME       AGE
---- ---------- ---
    5 Abi        22
```

### 4. Inserting Data

Insert a new employee record. Note that the first attempt fails due to incorrect syntax (double quotes instead of single quotes for the `NAME` column):

```sql
SQL> INSERT INTO employee VALUES (5, "Abi", 22);
ERROR at line 1:
ORA-00984: column not allowed here
```

Corrected insert statement:

```sql
SQL> INSERT INTO employee VALUES (5, 'Abi', 22);
1 row created.
```

### 5. Updating Data

Update the name of the employee with `ID = 5`. The first attempt fails due to missing the `SET` keyword:

```sql
SQL> UPDATE employee name = "Abishek" WHERE id = "5";
ERROR at line 1:
ORA-00971: missing SET keyword
```

Corrected update statement:

```sql
SQL> UPDATE employee
     SET name = 'Abishek'
     WHERE id = 5;
1 row updated.
```

Another update to change the name to 'Abiii':

```sql
SQL> UPDATE employee
     SET name = 'Abiii'
     WHERE id = 5;
1 row updated.
```

### 6. Flashback Queries

Use Oracle's flashback features to query historical data.

- Query the `EMPLOYEE` table as of a specific timestamp:

```sql
SQL> SELECT *
     FROM employee
     AS OF TIMESTAMP TO_TIMESTAMP('2025-08-20 08:00:00', 'YYYY-MM-DD HH24:MI:SS');
   ID NAME       AGE
---- ---------- ---
    4 Rama       23
    1 Ram        25
```

- Query the `EMPLOYEE` table as of 5 minutes ago:

```sql
SQL> SELECT * FROM employee AS OF TIMESTAMP (SYSTIMESTAMP - INTERVAL '5' MINUTE);
   ID NAME       AGE
---- ---------- ---
    4 Rama       23
    1 Ram        25
```

- Query the `NAME` column for `ID = 5` as of a specific SCN (System Change Number):

```sql
SQL> SELECT name FROM employee AS OF SCN 2398505 WHERE id = 5;
NAME
----------
Abishek
```

- Track changes to the `NAME` column for `ID = 5` using the `VERSIONS BETWEEN` clause:

```sql
SQL> SELECT versions_startscn,
            versions_endscn,
            versions_xid,
            name
     FROM employee VERSIONS BETWEEN SCN MINVALUE AND MAXVALUE
     WHERE id = 5;
VERSIONS_STARTSCN VERSIONS_ENDSCN VERSIONS_XID      NAME
----------------- --------------- ----------------- ----------
          2400437                           05001B00A9030000 Abiii
          2398499         2400437 09000E00AA030000 Abishek
```

### 7. Database Administration

Check the current SCN as a `SYSDBA` user:

```sql
SQL> connect / as sysdba;
Connected.
SQL> SELECT current_scn FROM v$database;
CURRENT_SCN
-----------
    2398505
```

### 8. Common Errors and Fixes

- **ORA-01017: invalid username/password**: Ensure correct credentials are provided.
- **ORA-00984: column not allowed here**: Use single quotes (`'`) for string literals instead of double quotes (`"`).
- **ORA-00971: missing SET keyword**: Include the `SET` keyword in `UPDATE` statements.
- **ORA-00906: missing left parenthesis**: Correct syntax for table names (e.g., remove colon in `TABLE:`).
- **ORA-01756: quoted string not properly terminated**: Ensure proper formatting in `TO_TIMESTAMP`.
- **ORA-00904: invalid identifier**: Correct typos (e.g., `MINIMAVLUE` to `MINVALUE`).
- **SP2-0734/SP2-0042**: Commands like `arhchive log list` or `ls` are invalid in SQL\*Plus. Use correct SQL or database commands.

## Notes

- The `EMPLOYEE` table must have versioning enabled (e.g., via `FLASHBACK ARCHIVE`) to support flashback queries.
- Always commit changes after `INSERT` or `UPDATE` operations to make them permanent:

```sql
SQL> COMMIT;
Commit complete.
```

- Use single quotes for string literals in Oracle SQL to avoid syntax errors.

## Conclusion

This README outlines the basic operations performed on the `EMPLOYEE` table, including data manipulation and historical data retrieval using Oracle's flashback features. Ensure proper syntax and permissions when executing these commands.

</xaiArtifact>
