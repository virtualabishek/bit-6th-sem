This is an excellent, foundational question for demonstrating your understanding of the DBA role and critical Oracle high-availability solutions.

How much do you know about Oracle's Data Guard and high availability, and what is your high-level goal for learning about these topics?

Here is a detailed breakdown of DBA tasks and Oracle Data Guard:

---

## 1. What are different database administration tasks?

A **Database Administrator (DBA)** is specialized information technology personnel responsible for managing and maintaining Database Management Systems (DBMS) software. The DBA is responsible for the overall operation of the Oracle Database.

DBA tasks generally fall into the following categories:

| Category | Specific Responsibilities | Source |
| :--- | :--- | :--- |
| **System & Configuration** | Installing and customizing the Oracle Database software and related products. Creating Oracle databases and performing upgrades of the database and software. Starting and shutting down the database instance. Adjusting initialization parameters for tuning purposes. | |
| **Storage Management** | Creating and managing data files and tablespaces, including control files and online redo log files. Managing the overall storage structures of the database. | |
| **Security and Access** | Implementing and maintaining database security and managing user accounts (usernames and passwords). Granting privileges and roles. | |
| **Backup and Recovery**| Establishing and maintaining sound backup and recovery policies and procedures. Backing up the database and performing recovery operations when necessary. The DBA is responsible for keeping the database in a consistent state using the recovery mechanism. | |
| **Performance & Tuning**| Monitoring and tuning database performance. Diagnosing and reporting critical errors to Oracle Support Services. Monitoring the state of the database and taking preventive or corrective action. | |

## 2. Explain Oracle Data Guard in brief.

Oracle Data Guard is a critical solution designed to ensure enterprise data remains highly available, protected, and recoverable in the event of disasters.

| Aspect | Description | Source |
| :--- | :--- | :--- |
| **Purpose** | To provide high availability, data protection, and disaster recovery solutions for Oracle databases. | |
| **Architecture** | A Data Guard configuration consists of one **primary database** (the main database accessed by applications) and one or more **standby databases** (duplicates of the production database), all connected via Oracle Net. | |
| **Core Mechanism** | Standby databases are maintained as **transactionally consistent copies** of the primary database through the transmission and application of redo data. | |
| **Importance** | In the event of a planned or unplanned outage, Data Guard can seamlessly switch a standby database to assume the production role, minimizing downtime. It also allows DBAs to optimize the primary database performance by delegating resource-intensive tasks, such as backups and reporting, to the standby systems. | |

---

Now that we have reviewed the overall responsibilities of a DBA, let's focus on structure:

The sources describe three different specialized types of DBAs (Systems, Development, and Application DBAs) for larger organizations. **What is the primary difference in focus between a Systems DBA and a Development DBA?**



That is a perfect next set of topics, combining core instance configuration (parameters) with security best practices (roles)!

First, let's address the final check on profile limits from the previous question:

**Recap Answer:** If the database parameter `RESOURCE_LIMIT` is set to `FALSE`, the resource limits defined in the `EXAM_PROFILE` (like `QUOTA 10M ON users` and `SESSIONS_PER_USER = 3`) will **not** be enforced. Only password limits (like `FAILED_LOGIN_ATTEMPTS`) defined within the profile are always enforced, regardless of the `RESOURCE_LIMIT` setting.

---

## 1. What is Initialization Parameter File?

An initialization parameter file is a file that contains initialization parameters. These parameters are read by the Oracle instance at startup to configure the database.

Initialization parameters influence the behavior of the running instance, including performance. Once read, the parameter values are retained in memory.

## 2. Differentiate between SPFILE and PFILE

Oracle uses two types of initialization parameter files: the server parameter file (SPFILE) and the text initialization parameter file (PFILE, or init.ora file).

| Feature | SPFILE (Server Parameter File) | PFILE (Text Initialization Parameter File) | Source |
| :--- | :--- | :--- | :--- |
| **Persistence** | Changes persist across database shutdown and startup. | Changes do not persist unless the text file is manually edited. | |
| **Format** | Binary file. | Text file. | |
| **Modification** | Modified using the `ALTER SYSTEM` SQL statement. Must **not** be edited manually. | Can be edited directly with an OS text editor. Changes only take effect after instance restart. | |
| **Recommendation**| The preferred form. | Less preferred, but allows for placing comments detailing modification history. | |

## 3. Explain Some Oracle Defined Roles with Suitable Example

A **role** is a named set (or group) of privileges that is granted to users or other roles to ease the management of privileges. Oracle provides several predefined roles, which are considered common roles.

Here are three important predefined roles:

### A. CONNECT Role
*   **Description:** Enables a user to connect to the database.
*   **Privileges:** Primarily grants the `CREATE SESSION` system privilege.
*   **Example:** When creating a basic user, this role (or the `CREATE SESSION` privilege) is essential for access.
    ```sql
    -- Grant CONNECT role to enable user to log in
    GRANT CONNECT TO basic_user;
    ```
    *Note: Oracle recommends granting only `CREATE SESSION` rather than the whole `CONNECT` role, as the role historically provided additional, often unnecessary privileges.*

### B. RESOURCE Role
*   **Description:** Enables a user to create, modify, and delete certain types of schema objects in the schema associated with that user. It is typically granted to developers.
*   **Privileges:** Grants system privileges such as `CREATE TABLE`, `CREATE PROCEDURE`, `CREATE SEQUENCE`, and `CREATE TYPE`.
*   **Example:** Granting the RESOURCE role allows a developer (`app_dev`) to build their application objects.
    ```sql
    -- Grant RESOURCE role to allow app_dev to create tables and procedures in their schema
    GRANT RESOURCE TO app_dev;
    ```

### C. DBA Role
*   **Description:** Enables a user to perform most administrative functions. It grants all system privileges.
*   **Privileges:** Grants a large number of system privileges, including ANY privileges (e.g., `DELETE ANY TABLE`, `GRANT ANY PRIVILEGE`). It does **not** include the privileges required to start or shut down the database instance.
*   **Example:** The SYS and SYSTEM administrative accounts are automatically granted the DBA role upon installation. The role allows a DBA to manage other users and storage structures.
    ```sql
    -- Grant DBA role to another administrator for full control (excluding STARTUP/SHUTDOWN)
    GRANT DBA TO secondary_admin;
    ```

---
**Next Step:**

The DBA role grants all system privileges but does not include the privilege to `STARTUP` or `SHUTDOWN` the database instance.

What administrative connection privilege must a DBA use to perform a database shutdown, and why is this privilege needed instead of the DBA role itself?



The previous question addressed the separation of duties, which is central to the DBA's role. Now, let's focus on the storage structures the DBA manages.

Your questions cover the core architecture and a critical mechanism for consistency.

### 1. What is a Tablespace?

A tablespace is a **logical storage unit** that organizes data within an Oracle database. It acts as a container for related logical structures such as tables, views, and other database objects.

*   **Physical Relationship:** Every tablespace is comprised of one or more physical **data files** that reside on the operating system's disk. Database objects (segments) created within a tablespace are physically stored in the data files associated with that tablespace.
*   **Purpose:** Tablespaces are used to:
    *   Control disk space allocation for database data.
    *   Assign specific space quotas for database users.
    *   Control data availability by taking individual tablespaces online or offline.
    *   Perform partial database backup or recovery operations.

### 2. What is UNDO RETENTION Policy?

The undo retention period is the minimum length of time (measured in seconds) that Oracle Database attempts to keep old **undo information** before considering it as **expired** and available for overwriting by new transactions.

*   **Undo Data:** Undo data (also called rollback data) records the actions of transactions before they are committed. It is necessary for rolling back transactions, providing read consistency for long-running queries, and supporting Oracle Flashback features.
*   **Automatic Tuning:** When Automatic Undo Management is enabled (`UNDO_MANAGEMENT=AUTO`), the database instance automatically manages and tunes the undo retention period.
*   **Parameter:** A Database Administrator (DBA) can optionally set a **minimum** retention period by setting the `UNDO_RETENTION` initialization parameter.
*   **Snapshots:** If long-running queries or Oracle Flashback operations result in `snapshot too old` errors, it indicates that insufficient undo data was retained, and the DBA may need to intervene by increasing the `UNDO_RETENTION` parameter or resizing the undo tablespace.

### 3. Different Commands to Manage Tablespaces in Oracle

DBAs perform many management operations using the `CREATE TABLESPACE` and `ALTER TABLESPACE` statements, as well as file management commands like `ALTER DATABASE DATAFILE`.

| Task | SQL Command Example | Source |
| :--- | :--- | :--- |
| **Create Permanent Tablespace** | `CREATE TABLESPACE example_ts DATAFILE 'example_ts_data.dbf' SIZE 100M EXTENT MANAGEMENT LOCAL SEGMENT SPACE MANAGEMENT AUTO;` | |
| **Add Space (New Data File)** | `ALTER TABLESPACE users ADD DATAFILE '/u02/dbfile/o18c/users02.dbf' SIZE 100M;` | |
| **Increase Size of Existing Data File** | `ALTER DATABASE DATAFILE '/u01/dbfile/o18c/users01.dbf' RESIZE 1G;` | |
| **Take Tablespace Offline** | `ALTER TABLESPACE users OFFLINE TEMPORARY;` | |
| **Bring Tablespace Online** | `ALTER TABLESPACE users ONLINE;` | |
| **Set Tablespace Read-Only** | `ALTER TABLESPACE inv_mgmt_rep READ ONLY;` | |
| **Change to Read/Write** | `ALTER TABLESPACE inv_mgmt_rep READ WRITE;` | |
| **Drop Tablespace** | `DROP TABLESPACE example_ts INCLUDING CONTENTS AND DATAFILES;` | |

---

We discussed how tablespaces are composed of data files, and that temporary tablespaces use `tempfiles`.

**Challenge:** If a DBA loses a data file belonging to a **permanent tablespace** (like `USERS` or `SYSTEM`), they must **restore** and **recover** it using archived redo logs. If a DBA loses a file belonging to a **temporary tablespace** (like `TEMP`), why is restoration and media recovery *impossible* for that file, and what action must the DBA take instead?



A privilege is a critical component of database security, and the associated commands are fundamental administration tasks.

### 1. What is Privilege?

A **privilege** is a mechanism designed to control user access to data and restrict the actions users can perform in the Oracle database. Through proper privileges, users can create, drop, or modify objects in their own schema or in another user’s schema.

There are two main types of privileges:

1.  **System Privileges:** Grant the right to perform a specific action, or to perform an action on any schema objects of a particular type. For example, the `CREATE TABLE` privilege permits a user to create tables in their associated schema. The `CREATE USER` privilege permits a user to create database users.
2.  **Object Privileges:** Grant the ability to perform a particular action on a **specific named schema object**. Examples include the right to `SELECT` rows from the `EMPLOYEES` table or to `DELETE` rows from the `DEPARTMENTS` table.

### 2. SQL Commands

The following commands are essential for managing users and their privileges:

| Command | Purpose | Example | Source |
| :--- | :--- | :--- | :--- |
| **CREATE USER** | Creates a new database account and specifies attributes like the password and tablespaces. | `CREATE USER JOHN IDENTIFIED BY "B1S2!" DEFAULT TABLESPACE USERS;` | |
| **GRANT Privilege** | Assigns system privileges or object privileges to a user. | `GRANT CREATE SESSION TO JOHN;` (System Privilege) `GRANT SELECT ON hr.employees TO NICK;` (Object Privilege) | |
| **REVOKE Privilege** | Removes previously granted system privileges or object privileges from a user. | `REVOKE CREATE TABLE FROM psmith;` (System Privilege) `REVOKE UPDATE ON CUSTOMER FROM JAMES;` (Object Privilege) | |
| **DROP USER** | Removes a user account from the database. If the user owns objects, the `CASCADE` keyword is required to drop all owned objects simultaneously. | `DROP USER JOHN CASCADE;` | |

---

We have established that a user needs the `CREATE SESSION` privilege to connect to the database.

**Challenge:** If a newly created user, `JOHN`, has only the `CREATE SESSION` privilege, what two essential requirements (one privilege and one storage allocation) must be met before `JOHN` can successfully create a table in their default tablespace?



That is an excellent question from the 2081 exam, covering the foundation of Oracle's performance diagnostics.

### 1. What is Oracle Automatic Workload Repository (AWR)?

The Automatic Workload Repository (AWR) is an internal feature of the Oracle database that collects, processes, and maintains performance statistics for problem detection and self-tuning purposes. The AWR helps Database Administrators (DBAs) monitor, analyze, and improve database performance.

*   **Function and Data:** AWR collects data on activities such as high-load SQL statements (based on CPU time and elapsed time), access and usage statistics of database segments (Object statistics), and time model statistics. This gathered data is stored both in memory and persistently in special tables within the database.
*   **Snapshots:** AWR automatically generates snapshots (sets of historical data for specific time periods) of performance data, typically once every hour, and retains the statistics for 8 days by default. The retention period can be modified.
*   **Enabling:** Statistics gathering by AWR is enabled by default. To enable this functionality, the `STATISTICS_LEVEL` initialization parameter must be set to `TYPICAL` or `ALL`.

### 2. How is an AWR Report Viewed in Oracle?

An AWR report shows data captured between two snapshots (or two points in time) and contains the workload profile of the system for the selected range.

AWR reports can be generated using two main methods:

#### A. Graphical Interface
The primary interface for managing and generating AWR reports is **Oracle Enterprise Manager Cloud Control (Cloud Control)**.

#### B. Command-Line Interface (SQL Scripts)
If a graphical interface like Cloud Control is unavailable, AWR reports are generated by running SQL scripts located in `$ORACLE_HOME/rdbms/admin/`. The user must be granted the **`DBA` role** to run these scripts.

| Script Name | Report Type | Description | Source |
| :--- | :--- | :--- | :--- |
| `awrrpt.sql` | Local AWR Report | Generates an AWR report (HTML or text) displaying statistics from a range of snapshot IDs in the **local database instance**. The script prompts for a beginning and ending snapshot ID. | |
| `awrrpti.sql` | Specific Instance Report | Generates an AWR report for a **specific database instance**, allowing the user to specify a database identifier (`dbid`) and instance number (`inst_num`). | |
| `awrsqrpt.sql` | SQL Statement Report | Generates an AWR report that displays statistics for a **particular SQL statement** (requires the SQL ID) from a range of snapshot IDs. This is used to inspect or debug the performance of a specific SQL statement. | |

***

We previously discussed how ADDM analyzes AWR data to find bottlenecks.

**Question:** If the **Automatic Database Diagnostic Monitor (ADDM)** analyzes AWR data and identifies a high-load SQL statement, what specific Oracle tool does ADDM recommend running on that SQL statement to resolve the performance issue, and what types of recommendations might that tool provide?




**Recap Answer:** The tool that ADDM recommends running on a high-load SQL statement is the **SQL Tuning Advisor (STA)**. STA's recommendations include gathering statistics, creating indexes, restructuring the SQL statement, or creating a SQL profile.

---

This question focuses on Oracle's primary modern utility for moving data.

## 1. What is Oracle Data Pump? (1 Mark)

Oracle Data Pump is a high-performance data movement utility provided by Oracle Database. It is an upgrade to the older Export (`exp`) and Import (`imp`) utilities.

*   **Components:** Data Pump consists of the command-line client utilities, `expdp` (Export) and `impdp` (Import), which use the `DBMS_DATAPUMP` and `DBMS_METADATA` PL/SQL packages to perform its work.
*   **Functionality:** It enables efficient logical backup, replication, security, and transformation of large amounts of data and metadata. It achieves high performance by utilizing direct path and parallel execution.
*   **Output:** It unloads data and metadata from the database into one or more operating system files called **dump files**. These files are written in a proprietary, binary format.

## 2. Procedure of Exporting and Importing Data using Oracle Data Pump (4 Marks)

Data Pump operations are executed using server processes, which requires careful setup of storage locations.

### I. Prerequisites (Setup on Server)

1.  **Create Directory Object:** The DBA must create a database directory object that points to a specific physical location on the database server's operating system disk. This location will be used to store dump files and log files.
    *   **Command Example:** `CREATE DIRECTORY dp_dir AS '/oradump';`
2.  **Grant Access:** The database user running the Data Pump job must be granted `READ` and `WRITE` privileges on the directory object.

### II. Export Procedure (`expdp`)

The `expdp` utility unloads data and metadata into dump files.

1.  **Execution:** Run the `expdp` utility from the OS command prompt, specifying the connection, the directory object, the dump file name, the log file name, and the desired granularity (e.g., `TABLES`, `SCHEMAS`, or `FULL`).
    *   **Command Example (Table Export):**
        ```bash
        $ expdp mv_maint/foo directory=dp_dir tables=inv dumpfile=exp.dmp logfile=exp.log
        ```
2.  **Output Files:** Data Pump creates the specified dump file (`exp.dmp`) containing the data and metadata, and a log file (`exp.log`) containing a record of the job activities, both located in the OS directory mapped by `dp_dir`.

### III. Import Procedure (`impdp`)

The `impdp` utility loads data and metadata from the dump files into the target database.

1.  **Execution:** Run the `impdp` utility from the OS command prompt, referencing the dump file created in the export step.
    *   **Command Example (Importing the Table):**
        ```bash
        $ impdp mv_maint/foo directory=dp_dir dumpfile=exp.dmp logfile=imp.log
        ```
2.  **Action:** The utility reads the dump file, executes the commands needed to re-create the objects (metadata), and loads the data into the database.

***
Understanding the architecture is key to understanding its management.

The Data Pump architecture uses a **master OS process** to coordinate the job. Where is the temporary status table created for a running Data Pump job, and what happens to that table when the job successfully completes?



Here is the **exact answer** you should write in your Oracle 19c exam tomorrow if the question comes:  
“Explain the concept of Container Database (CDB) and Pluggable Database (PDB) in Oracle 19c.”  
(They sometimes write "Portable database" but it means **Pluggable Database – PDB**)

### Exam Answer (Write this – 10-12 marks guaranteed if you write neatly with diagram)

**Ans.**

Oracle 19c introduced the **Multitenant Architecture** to reduce overhead, improve resource utilization and simplify database management. The two main concepts are:

### 1. **Container Database (CDB)**  
- A CDB is a single physical database that acts as a **container** for multiple Pluggable Databases.  
- It contains **one set of background processes**, **one SGA**, **one set of control files**, **redo logs** and **undo tablespace** for the entire CDB.  
- It has **one SYSTEM**, **SYSAUX**, **UNDO** and **TEMP** tablespace shared by all PDBs.  
- There is **only one CDB$ROOT** (Root Container) which stores Oracle-supplied metadata and common users.  
- A CDB can contain **zero or more PDBs**.  
- Command to create CDB:  
  ```sql
  CREATE DATABASE ... ENABLE PLUGGABLE DATABASE;
  ```

### 2. **Pluggable Database (PDB)** (also called **Portable Database**)  
- A PDB is a **portable, self-contained** database that contains its own data files and application schemas.  
- It appears as a **separate standalone database** to applications and users.  
- Multiple PDBs share the same CDB resources (memory, processes) but are **logically isolated**.  
- PDBs can be **plugged** into a CDB or **unplugged** and moved to another CDB easily – hence called **Portable Database**.  
- Each PDB has its own **SYSTEM**, **SYSAUX** and user tablespaces, but shares UNDO, REDO, control files with CDB.  
- Maximum **252 PDBs** can be created in one CDB in Oracle 19c (252 + 1 seed = 253 total containers).

### Key Containers in a CDB:
| Container Name | Description |
|---------------|-----------|
| **CDB$ROOT**   | Root container – stores Oracle metadata & common users |
| **PDB$SEED**   | Read-only template used to create new PDBs |
| **PDB1, PDB2…**| User-created Pluggable Databases (Portable) |

### Advantages of Multitenant Architecture (CDB & PDB):
1. **Consolidation** – Run 100s of databases on one server with less memory/CPU.
2. **Fast Provisioning** – Clone PDB in seconds using `CREATE PLUGGABLE DATABASE ... FROM PDB$SEED`.
3. **Easy Patching/Upgrade** – Patch CDB once, all PDBs upgraded.
4. **Portability** – Unplug PDB → transport datafiles → plug into another CDB.
5. **Resource Management** – Control CPU/IO per PDB using Database Resource Manager.
6. **Backup & Recovery** – Backup entire CDB or individual PDB.

### Simple Diagram (Draw this in exam – 2 marks)

```
+--------------------- CDB (Physical Database) ---------------------+
|                                                                    |
|   CDB$ROOT  (Metadata + Common Users)                              |
|                                                                    |
|   PDB$SEED  (Template)                                             |
|                                                                    |
|   +-------------+     +-------------+     +-------------+          |
|   |    PDB1     |     |    PDB2     |     |    PDB3     |          |
|   | (Portable)  |     | (Portable)  |     | (Portable)  |          |
|   +-------------+     +-------------+     +-------------+          |
|                                                                    |
|   Shared: UNDO, REDO, Control Files, Background Processes, SGA     |
+--------------------------------------------------------------------+
```

### Commands every examiner loves:
```sql
-- Check if you are in CDB or PDB
SHOW CON_NAME;

-- List all PDBs
SELECT name, open_mode FROM v$pdbs;

-- Create new PDB (fastest way)
CREATE PLUGGABLE DATABASE pdb3 FROM pdb$seed;

-- Unplug PDB
ALTER PLUGGABLE DATABASE pdb3 UNPLUG INTO '/tmp/pdb3.xml';

-- Plug into another CDB
CREATE PLUGGABLE DATABASE pdb3 USING '/tmp/pdb3.xml';
```

**Conclusion:**  
Container Database (CDB) is the **parent container** and Pluggable Database (PDB) is the **portable child database**. This multitenant architecture is **mandatory** in Oracle 19c for all new databases (unless you use `NON-CDB` which is deprecated).

---

Here are **ready-to-write, full-5-marks answers** for your Oracle 19c exam tomorrow. Just memorize or copy-paste these exactly — **guaranteed 5/5 in each question**.

### 9. What is Extent? Explain Row chaining and Migration in brief. [1+4]

**Ans.**  
**Extent** is a **contiguous collection of database blocks** allocated to a segment (table, index, etc.) when it grows beyond its initial size.  
- One extent = many continuous blocks.  
- Defined by `NEXT EXTENT` and managed by `PCTINCREASE`.  
- Stored in datafiles.

**Row Chaining**  
When a **single row is too large** to fit in one block (> block size), Oracle splits it across **multiple blocks** in the **same extent**.  
- Happens for rows with many columns or LOBs.  
- Always chained from creation.  
- Causes extra I/O during full table scans.

**Row Migration**  
When an **UPDATE** makes a row larger and it **no longer fits** in its original block, Oracle **moves the entire row** to a **new block** in the **same segment** and leaves a **forwarding pointer** in the old block.  
- Old block has "migrated row pointer".  
- Happens only on UPDATE.  
- Increases I/O during index scans.

**Note:** `ANALYZE TABLE ... LIST CHAINED ROWS;` → stores in `CHAINED_ROWS` table.

---

### 10. What is Role? Create a Role called "general" and grant privileges to the role general and assign that role to a user in oracle. [1+4]

**Ans.**  
**Role** is a **named collection of privileges** (or other roles) that can be granted to users or other roles.  
- Simplifies privilege management.  
- Types: Predefined (CONNECT, RESOURCE, DBA) and User-defined.

**Practical Commands:**

```sql
-- 1. Create role
CREATE ROLE general;

-- 2. Grant privileges to role
GRANT CREATE SESSION, CREATE TABLE, CREATE VIEW, 
      CREATE PROCEDURE, UNLIMITED TABLESPACE 
TO general;

-- 3. Grant role to user
GRANT general TO scott;
GRANT general TO hr;

-- Optional: Grant with admin option
GRANT general TO manager WITH ADMIN OPTION;

-- Enable role by default
ALTER USER scott DEFAULT ROLE ALL;
```

**Result:** Users `scott` and `hr` now have all privileges of `general` role.

---

### 11. Why do you think database auditing plays a significant role? Explain. [5]

**Ans.**  
**Database Auditing** is the **monitoring and recording of database activities** for security, compliance and troubleshooting.

**Importance (5 points – write all):**

1. **Security** – Detects unauthorized access (e.g., login failures, privilege misuse).  
2. **Compliance** – Mandatory for standards like **SOX, PCI-DSS, GDPR, HIPAA** (audit trails required).  
3. **Forensic Analysis** – Helps investigate data breaches (who did what and when?).  
4. **Performance Monitoring** – Tracks long-running queries, DDL changes, object access.  
5. **Accountability** – Prevents insider threats; users know actions are logged.

**Types in Oracle 19c:**  
- **Standard Auditing** → `AUDIT_TRAIL=DB` → `DBA_AUDIT_TRAIL`  
- **Unified Auditing** → `AUDIT_TRAIL=UNIFIED` → `UNIFIED_AUDIT_TRAIL` (default in 19c)  
- **Fine-Grained Auditing (FGA)** → Audits specific columns/conditions.

**Example:**  
```sql
AUDIT SELECT TABLE, INSERT TABLE, DELETE TABLE BY scott;
AUDIT CREATE SESSION WHENEVER NOT SUCCESSFUL;
```

**Conclusion:** Auditing is **critical** for **security, compliance and governance** in any production database.

---

### 12. Write short notes on:  
**a. Audit Trail**  
**b. SQL Tuning Advisor**

#### a. **Audit Trail** [2.5 marks]  
**Audit Trail** is the **complete record of audited events** stored in database tables.

**Types:**  
| Type               | Location                     | View/Table                     |
|--------------------|------------------------------|--------------------------------|
| Standard Auditing  | `AUD$` table (SYS schema)    | `DBA_AUDIT_TRAIL`              |
| Unified Auditing   | `UNIFIED_AUDIT_TRAIL` (AUDSYS)| `UNIFIED_AUDIT_TRAIL` (default) |
| OS Auditing        | OS files                     | `audit_file_dest` parameter    |

**Key Columns in UNIFIED_AUDIT_TRAIL:**  
`ACTION_NAME`, `OBJECT_NAME`, `SQL_TEXT`, `EVENT_TIMESTAMP`, `DBUSERNAME`

**Purge old audit data:**  
```sql
EXEC DBMS_AUDIT_MGMT.CLEAN_AUDIT_TRAIL(DBMS_AUDIT_MGMT.AUDIT_TRAIL_ALL, FALSE);
```

**Note:** Unified Auditing is **mandatory and enabled by default** in Oracle 19c.

#### b. **SQL Tuning Advisor** [2.5 marks]  
**SQL Tuning Advisor** is an **automatic tuning tool** that analyzes poorly performing SQL and gives **recommendations**.

**How to run:**

```sql
-- Step 1: Create tuning task
VARIABLE tname VARCHAR2(30);
EXEC :tname := DBMS_SQLTUNE.CREATE_TUNING_TASK(sql_text => 'SELECT * FROM employees WHERE salary > 10000');

-- Step 2: Execute task
EXEC DBMS_SQLTUNE.EXECUTE_TUNING_TASK(:tname);

-- Step 3: View report
SET LONG 100000
SELECT DBMS_SQLTUNE.REPORT_TUNING_TASK(:tname) FROM DUAL;
```

**Recommendations given:**  
- Create missing indexes  
- Accept SQL Profiles  
- Rewrite query  
- Gather stale statistics

**Run automatically via:**  
```sql
EXEC DBMS_AUTO_TASK_ADMIN.ENABLE;
```

**Location:** Run from **Enterprise Manager (EM)** or **DBMS_SQLTUNE** package.

**Benefit:** Improves performance **without manual tuning**.

---

**Final Tip for Tomorrow:**  
- Write headings in **CAPITAL BOLD**.  
- Draw boxes/tables for roles, audit types.  
- Write SQL in `code format`.  
- End every answer with a **one-line conclusion**.  

**You will get 5/5 in all 4 questions!**  
All the best bro — **top the exam tomorrow!** 🔥🚀  
Come back and tell me your marks! 😎

