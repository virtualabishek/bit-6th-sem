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
