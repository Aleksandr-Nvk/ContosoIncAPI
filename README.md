# ContosoIncAPI

## Step-1 - Create test DB

The database is created on MySQL. Here's the code used to create tables and content:

```
# Dynamics of user registrations by month:

CREATE TABLE usr_by_mnth (
 id INT PRIMARY KEY AUTO_INCREMENT,
 yr INT UNSIGNED,
 mnth ENUM('December', 'January', 'February', 'March',
 		   'April', 'May', 'June', 'July',
 		   'August', 'September', 'October', 'November'),
 usr_num INT UNSIGNED
);

DROP TABLE usr_by_mnth;

INSERT INTO usr_by_mnth VALUES(0, 2020, 'August', 13);
INSERT INTO usr_by_mnth(yr, mnth, usr_num) VALUE(2020, 'September', 5);
INSERT INTO usr_by_mnth(yr, mnth, usr_num) VALUE(2020, 'October', 7);
INSERT INTO usr_by_mnth(yr, mnth, usr_num) VALUE(2021, 'January', 9);
INSERT INTO usr_by_mnth(yr, mnth, usr_num) VALUE(2021, 'February', 6);
INSERT INTO usr_by_mnth(yr, mnth, usr_num) VALUE(2021, 'July', 32);

SELECT * FROM usr_by_mnth;

# Dynamics of device registrations by device type and month:

CREATE TABLE dev_by_type_mnth (
 id INT PRIMARY KEY AUTO_INCREMENT,
 yr INT UNSIGNED,
 mnth ENUM('December', 'January', 'February', 'March',
 		   'April', 'May', 'June', 'July',
 		   'August', 'September', 'October', 'November'),
 dev_type ENUM('Laptop', 'Mobile phone', 'Tablet'),
 usr_num INT UNSIGNED
);

DROP TABLE dev_by_type_mnth;

INSERT INTO dev_by_type_mnth VALUES(0, 2020, 'August', 'Laptop', 10);
INSERT INTO dev_by_type_mnth(yr, mnth, dev_type, usr_num) VALUE(2020, 'August', 'Mobile phone', 3);
INSERT INTO dev_by_type_mnth(yr, mnth, dev_type, usr_num) VALUE(2020, 'September', 'Laptop', 5);
INSERT INTO dev_by_type_mnth(yr, mnth, dev_type, usr_num) VALUE(2021, 'July', 'Laptop', 15);
INSERT INTO dev_by_type_mnth(yr, mnth, dev_type, usr_num) VALUE(2021, 'July', 'Mobile phone', 8);
INSERT INTO dev_by_type_mnth(yr, mnth, dev_type, usr_num) VALUE(2021, 'July', 'Tablet', 9);

SELECT * FROM dev_by_type_mnth;

# List of concurrent login sessions for every hour:

CREATE TABLE conc_sess_by_hour (
 id INT PRIMARY KEY AUTO_INCREMENT,
 hr DATETIME,
 sess_num INT UNSIGNED
);

DROP TABLE conc_sess_by_hour;

INSERT INTO conc_sess_by_hour VALUES(0, '2021-07-01 13:00:00', 3);
INSERT INTO conc_sess_by_hour(hr, sess_num) VALUE('2021-07-01 14:00:00', 23);
INSERT INTO conc_sess_by_hour(hr, sess_num) VALUE('2021-07-01 15:00:00', 19);
INSERT INTO conc_sess_by_hour(hr, sess_num) VALUE('2021-07-01 16:00:00', 10);
INSERT INTO conc_sess_by_hour(hr, sess_num) VALUE('2021-07-01 17:00:00', 15);
INSERT INTO conc_sess_by_hour(hr, sess_num) VALUE('2021-07-01 18:00:00', 8);

SELECT * FROM conc_sess_by_hour;

# List of users and devices concurrenty logged in from more than 1 device:

CREATE TABLE usr_dev_cur_mult (
 id INT PRIMARY KEY AUTO_INCREMENT,
 usr_name VARCHAR(30),
 dev_name VARCHAR(30),
 login_ts DATETIME
);

DROP TABLE usr_dev_cur_mult;

INSERT INTO usr_dev_cur_mult VALUES(0, 'John Doe', 'John\'s Laptop', '2021-07-01 17:35:18');
INSERT INTO usr_dev_cur_mult(usr_name, dev_name, login_ts) VALUE('John Doe', 'John\'s Mobile phone', '2021-07-01 17:35:18');
INSERT INTO usr_dev_cur_mult(usr_name, dev_name, login_ts) VALUE('Kathy Johnson', 'My Macbook', '2021-07-01 18:11:23');
INSERT INTO usr_dev_cur_mult(usr_name, dev_name, login_ts) VALUE('Kathy Johnson', 'My IPhone', '2021-07-01 18:13:26');
INSERT INTO usr_dev_cur_mult(usr_name, dev_name, login_ts) VALUE('Kathy Johnson', 'My IPad', '2021-07-01 18:29:31');


SELECT * FROM usr_dev_cur_mult;

# Total accumulated session duration by hour:

CREATE TABLE sess_dur_by_hour (
 id INT PRIMARY KEY AUTO_INCREMENT,
 date DATE,
 hr INT UNSIGNED,
 dur INT UNSIGNED,
 dur_acum INT UNSIGNED
);

DROP TABLE sess_dur_by_hour;

INSERT INTO sess_dur_by_hour VALUES(0, '2021-06-29', 0, 500, 500);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-06-29', 1, 342, 842);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-06-29', 2, 100, 942);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-06-29', 23, 154, 15643);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-06-30', 0, 100, 100);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-06-30', 1, 200, 300);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-07-01', 0, 450, 450);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-07-01', 1, 200, 650);
INSERT INTO sess_dur_by_hour(date, hr, dur, dur_acum) VALUE('2021-07-01', 18, 100, 21350);


SELECT * FROM sess_dur_by_hour;

# List of users and countries users logged in from today that were not previosly seen as a country of log in:

CREATE TABLE usr_cntry_uns (
 id INT PRIMARY KEY AUTO_INCREMENT,
 usr_name VARCHAR(30),
 country VARCHAR(30),
 login_ts DATETIME
);

DROP TABLE usr_cntry_uns;

INSERT INTO usr_cntry_uns VALUES(0, 'John Doe', 'Switzerland', '2021-07-01 17:35:18');
INSERT INTO usr_cntry_uns(usr_name, country, login_ts) VALUE('Kathy Johnson', 'Turkey', '2021-07-01 18:11:23');


SELECT * FROM usr_cntry_uns;
```
