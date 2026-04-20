--to drop database 
-- use master;
-- GO
-- alter database MyfirstDB set Single_user with ROLLBACK IMMEDIATE;
-- GO
-- drop database MyfirstDB;
-- GO

CREATE DATABASE MyfirstDB;
use MyfirstDB;
GO
CREATE TABLE employee (
	emp_id INT PRIMARY KEY,
	first_name VARCHAR(40),
	last_name VARCHAR(40),
	birth_date DATE,
	sex VARCHAR (1),
	salary INT,
	super_id INT,
	Branch_id INT
);

--Branch Table

CREATE TABLE Branch (
    Branch_id INT PRIMARY KEY,
    Branch_name VARCHAR(40),
    mgr_id INT,
    mgr_start_date DATE,
    FOREIGN KEY (mgr_id) REFERENCES employee(emp_id) ON DELETE SET NULL
);

--ADDING FOREIGN KEY (Branch_id) TO EMPLOYEE TABLE

-- One Branch → Many Employees
-- (1:N relationship)

ALTER TABLE employee
ADD FOREIGN KEY (Branch_id)
REFERENCES Branch(Branch_id)
ON DELETE SET NULL;

--ADDING FOREIGN KEY(super_id) TO EMPLOYEE TABLE (self relationship)

-- An employee can have a supervisor.
-- Supervisor is also an employee.
-- If supervisor is deleted → super_id becomes NULL.

ALTER TABLE employee
ADD FOREIGN KEY (super_id)
REFERENCES employee(emp_id)
ON DELETE NO ACTION;

--Client Table

CREATE TABLE Client(
	client_id INT PRIMARY KEY,
	client_name VARCHAR(40),
	Branch_id INT,
	FOREIGN KEY (Branch_id) REFERENCES Branch(Branch_id) ON DELETE SET NULL
);


--works_with Table  (many to many relationship)
--PRIMARY KEY(emp_id, client_id) composite key (One employee can work with many clients 
--One client can work with many employees)

CREATE TABLE works_with(
	emp_id INT,
	client_id INT,
	total_sales INT,
	PRIMARY KEY(emp_id, client_id),
	FOREIGN KEY(emp_id) REFERENCES employee(emp_id) ON DELETE CASCADE,
	FOREIGN KEY(client_id) REFERENCES Client(client_id) ON DELETE CASCADE
);


--Branch supplier TABLE
CREATE TABLE Branch_supplier(
Branch_id INT,
supplier_name VARCHAR(40),
supply_type VARCHAR(40),
PRIMARY KEY (Branch_id, supplier_name),
FOREIGN KEY (Branch_id) REFERENCES Branch(Branch_id) ON DELETE CASCADE
);

--To View tables
SELECT * FROM sys.tables;

--Branch
  ---< Employee
  ---< Client
  ---< Branch_Supplier

--Employee
  ---< works_with >--- Client
  --- supervises (self relationship)

select * from employee;
-- inserting the values:

--corporate


INSERT INTO employee VALUES (100, 'Shine' , 'Selorm' , '2001-12-06' , 'M' , 250000 , NULL , NULL);

INSERT INTO branch VALUES (1, 'Accra' , '100' , '2006-09-14');

UPDATE employee SET branch_id = 1 WHERE emp_id = 100;

INSERT INTO employee VALUES (101, 'Stephanie', 'Danso' , '2004-4-13', 'F' , 11000 , 100 , 1 );


--Scrancom


INSERT INTO employee VALUES(102, 'Michael', 'Scott', '1964-03-15', 'M', 75000, 100, NULL);

INSERT INTO branch VALUES(2, 'Scranton', 102, '1992-04-06');

UPDATE employee
SET branch_id = 2
WHERE emp_id = 102;

INSERT INTO employee VALUES(103, 'Angela', 'Martin', '1971-06-25', 'F', 63000, 102, 2);
INSERT INTO employee VALUES(104, 'Kelly', 'Kapoor', '1980-02-05', 'F', 55000, 102, 2);
INSERT INTO employee VALUES(105, 'Stanley', 'Hudson', '1958-02-19', 'M', 69000, 102, 2);


-- Stamford


INSERT INTO employee VALUES(106, 'Josh', 'Porter', '1969-09-05', 'M', 78000, 100, NULL);

INSERT INTO branch VALUES(3, 'Stamford', 106, '1998-02-13');

UPDATE employee
SET branch_id = 3
WHERE emp_id = 106;

INSERT INTO employee VALUES(107, 'Andy', 'Bernard', '1973-07-22', 'M', 65000, 106, 3);
INSERT INTO employee VALUES(108, 'Jim', 'Halpert', '1978-10-01', 'M', 71000, 106, 3);


-- BRANCH SUPPLIER


INSERT INTO branch_supplier VALUES(2, 'Hammer Mill', 'Paper');
INSERT INTO branch_supplier VALUES(2, 'Uni-ball', 'Writing Utensils');
INSERT INTO branch_supplier VALUES(3, 'Patriot Paper', 'Paper');
INSERT INTO branch_supplier VALUES(2, 'J.T. Forms & Labels', 'Custom Forms');
INSERT INTO branch_supplier VALUES(3, 'Uni-ball', 'Writing Utensils');
INSERT INTO branch_supplier VALUES(3, 'Hammer Mill', 'Paper');
INSERT INTO branch_supplier VALUES(3, 'Stamford Lables', 'Custom Forms');


-- CLIENT


INSERT INTO client VALUES(400, 'Dunmore Highschool', 2);
INSERT INTO client VALUES(401, 'Lackawana Country', 2);
INSERT INTO client VALUES(402, 'FedEx', 3);
INSERT INTO client VALUES(403, 'John Daly Law, LLC', 3);
INSERT INTO client VALUES(404, 'Scranton Whitepages', 2);
INSERT INTO client VALUES(405, 'Times Newspaper', 3);
INSERT INTO client VALUES(406, 'FedEx', 2);


-- WORKS_WITH


INSERT INTO works_with VALUES(105, 400, 55000);
INSERT INTO works_with VALUES(102, 401, 267000);
INSERT INTO works_with VALUES(108, 402, 22500);
INSERT INTO works_with VALUES(107, 403, 5000);
INSERT INTO works_with VALUES(108, 403, 12000);
INSERT INTO works_with VALUES(105, 404, 33000);
INSERT INTO works_with VALUES(107, 405, 26000);
INSERT INTO works_with VALUES(102, 406, 15000);
INSERT INTO works_with VALUES(105, 406, 130000);



select * from works_with;

--Find names of all employees who have sold over 50,000 to a single client
SELECT distinct e.first_name, e.last_name
FROM employee as e 
WHERE e.emp_id IN (
    SELECT emp_id FROM works_with  WHERE total_sales > 50000);

--Find a list of employee and branch names --(00:00:00.038 non-indexed query)
CREATE NONCLUSTERED INDEX idx_employee_branch_id ON employee(Branch_id);
SELECT first_name, Branch_name from employee , Branch 
where employee.Branch_id=branch.Branch_id;
SELECT * from employee;
SELECT * FROM Branch;

SELECT getdate() as today_date ;

--Indexing is very important for performance optimization. 
-- It allows the database engine to find and retrieve data faster.
--Without an index, the database engine has to scan the entire table to find the relevant rows
-- Indexing is a special data structure.  
-- They uses B-Tree or Hashing to organize the data in a way that allows for fast retrieval.
-- They use logrithimic time complexity O(log n) for search instead of Linear time .
-- 
Create  INDEX idx_employee_emp_id ON employee(emp_id);
