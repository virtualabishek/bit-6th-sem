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
