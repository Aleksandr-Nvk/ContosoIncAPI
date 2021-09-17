# Dynamics of user registrations by month:

CREATE TABLE registrations_by_month (
 year YEAR,
 month ENUM('December', 'January', 'February', 'March', 'April', 'May', 
 		   'June', 'July', 'August', 'September', 'October', 'November'),
 users_num INT UNSIGNED,
 PRIMARY KEY(year, month)
);

INSERT INTO registrations_by_month VALUES(2020, 'August', 13);
INSERT INTO registrations_by_month VALUES(2020, 'September', 5);
INSERT INTO registrations_by_month VALUES(2020, 'October', 7);
INSERT INTO registrations_by_month VALUES(2021, 'January', 9);
INSERT INTO registrations_by_month VALUES(2021, 'February', 6);
INSERT INTO registrations_by_month VALUES(2021, 'July', 32);

SELECT * FROM registrations_by_month;

# Dynamics of device registrations by device type and month:

CREATE TABLE device_registrations_by_type_and_month (
 year YEAR,
 month ENUM('December', 'January', 'February', 'March',
      'April', 'May', 'June', 'July',
      'August', 'September', 'October', 'November'),
 device_type ENUM('Laptop', 'Mobile phone', 'Tablet'),
 users_num INT UNSIGNED,
 PRIMARY KEY(year, month, device_type)
);

INSERT INTO device_registrations_by_type_and_month VALUES(2020, 'August', 'Laptop', 10);
INSERT INTO device_registrations_by_type_and_month VALUES(2020, 'August', 'Mobile phone', 3);
INSERT INTO device_registrations_by_type_and_month VALUES(2020, 'September', 'Laptop', 5);
INSERT INTO device_registrations_by_type_and_month VALUES(2021, 'July', 'Laptop', 15);
INSERT INTO device_registrations_by_type_and_month VALUES(2021, 'July', 'Mobile phone', 8);
INSERT INTO device_registrations_by_type_and_month VALUES(2021, 'July', 'Tablet', 9);

SELECT * FROM device_registrations_by_type_and_month;

# List of concurrent login sessions for every hour:

CREATE TABLE concurrent_sessions_by_hour (
 hour DATETIME,
 sessions_num INT UNSIGNED,
 PRIMARY KEY(hour)
);

INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 13:00:00', 3);
INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 14:00:00', 23);
INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 15:00:00', 19);
INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 16:00:00', 10);
INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 17:00:00', 15);
INSERT INTO concurrent_sessions_by_hour VALUES('2021-07-01 18:00:00', 8);

SELECT * FROM concurrent_sessions_by_hour;

# List of users and devices concurrenty logged in from more than 1 device:

CREATE TABLE concurrent_users_from_multiple_devices (
 user_name VARCHAR(30),
 device_name VARCHAR(30),
 login_ts DATETIME,
 PRIMARY KEY(device_name, login_ts)
);

INSERT INTO concurrent_users_from_multiple_devices VALUES('John Doe', 'John\'s Laptop', '2021-07-01 17:35:18');
INSERT INTO concurrent_users_from_multiple_devices VALUES('John Doe', 'John\'s Mobile phone', '2021-07-01 17:35:18');
INSERT INTO concurrent_users_from_multiple_devices VALUES('Kathy Johnson', 'My Macbook', '2021-07-01 18:11:23');
INSERT INTO concurrent_users_from_multiple_devices VALUES('Kathy Johnson', 'My IPhone', '2021-07-01 18:13:26');
INSERT INTO concurrent_users_from_multiple_devices VALUES('Kathy Johnson', 'My IPad', '2021-07-01 18:29:31');

SELECT * FROM concurrent_users_from_multiple_devices;

# Total accumulated session duration by hour:

CREATE TABLE session_duration_by_hour (
 date DATE,
 hour INT UNSIGNED,
 duration INT UNSIGNED,
 duration_accumulated INT UNSIGNED,
 PRIMARY KEY(date, hour)
);

INSERT INTO session_duration_by_hour VALUES('2021-06-29', 0, 500, 500);
INSERT INTO session_duration_by_hour VALUES('2021-06-29', 1, 342, 842);
INSERT INTO session_duration_by_hour VALUES('2021-06-29', 2, 100, 942);
INSERT INTO session_duration_by_hour VALUES('2021-06-29', 23, 154, 15643);
INSERT INTO session_duration_by_hour VALUES('2021-06-30', 0, 100, 100);
INSERT INTO session_duration_by_hour VALUES('2021-06-30', 1, 200, 300);
INSERT INTO session_duration_by_hour VALUES('2021-07-01', 0, 450, 450);
INSERT INTO session_duration_by_hour VALUES('2021-07-01', 1, 200, 650);
INSERT INTO session_duration_by_hour VALUES('2021-07-01', 18, 100, 21350);

SELECT * FROM session_duration_by_hour;

# List of users and countries users logged in from today that were not previosly seen as a country of log in:

CREATE TABLE users_by_unseen_country (
 user_name VARCHAR(30),
 country VARCHAR(30),
 login_ts DATETIME,
 PRIMARY KEY(login_ts)
);

INSERT INTO users_by_unseen_country VALUES('John Doe', 'Switzerland', '2021-07-01 17:35:18');
INSERT INTO users_by_unseen_country VALUES('Kathy Johnson', 'Turkey', '2021-07-01 18:11:23');

SELECT * FROM users_by_unseen_country;