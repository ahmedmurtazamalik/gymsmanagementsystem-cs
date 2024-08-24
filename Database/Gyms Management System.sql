create database project;
use project;

create table users(
    userid int identity(1,1) not null,
    username varchar(255) unique not null,
    password varchar(255) not null,
    fname varchar(255) not null,
    lname varchar(255) not null,
    contact varchar(30) not null,
    userType varchar(10) not null,
    isActive bit not null default 1, -- Adding isActive column
    primary key (userid)
)

create table dietplan
(
	dietplanId int identity(1,1) primary key,
	dietplanName varchar(255) not null,	
	nutriValue int not null,
	[type] varchar(100) not null,
	purpose varchar(100) not null,
)

create table workoutPlan
(
	workoutPlanID int identity(1,1) Primary Key,
	workoutplanName varchar(255) not null,
	[sets] int not null,
	reps int not null,	
);

CREATE TABLE GYM(
	GymId INT identity(1,1) PRIMARY KEY, 
	gymName VARCHAR(100) unique,
	[Location] VARCHAR(100),
	Members INT,
    isApproved bit not null default 0,
	AdminID INT,
	OwnerID INT
	FOREIGN KEY (AdminId) References [users](userId),
	FOREIGN KEY (OwnerId) References [users](userId)
);

create table member(
	memberid int not null,
	dietplanid int,
	workoutplanid int,
	membership varchar(40) not null,
	gymName varchar(100),
	foreign key (gymName) references GYM(gymName),
	foreign key (memberid) references users(userid),
	foreign key (dietplanid) references dietplan(dietplanid),
	foreign key (workoutplanid) references workoutplan(workoutplanid)
)

create table owner(
	ownerid int not null,
	
	foreign key (ownerid) references users(userid)
)

create table trainer(
	trainerid int not null,
	ownerid int,
	foreign key (trainerid) references users(userid),
	foreign key (ownerid) references users(userid),
)

create table feedback(
	feedbackid int identity(1,1) not null,
	memberid int not null,
	trainerid int not null,
	dateandtime datetime not null,
	rating int not null,
	review varchar(255) not null,
	primary key (feedbackid),
	foreign key (memberid) references users(userid),
	foreign key (trainerid) references users(userid)
);

create table trainer_gym(
	trainerid int not null,
	gymid int not null,
	startdate datetime,
	foreign key (trainerid) references users(userid),
	foreign key (gymid) references gym(gymid)	
)


create table trainer_request(
	requestid int identity(1,1) not null primary key,
	trainerid int not null,
	gymid int not null,
	status varchar(20) not null default 'Pending',
	foreign key (trainerid) references users(userid),
	foreign key (gymid) references gym(gymid)	
)

create table appointment(
	appointmentid int identity(1,1) not null,
	trainerid int not null,
	memberid int not null,
    isAccepted bit not null default 0, 
	date datetime not null,
	status varchar(255),
	primary key (appointmentid),
	foreign key (trainerid) references users(userid),
	foreign key (memberid) references users(userid)
)

create table [admin]
(
	adminid int Primary Key,
	Foreign Key (adminid) references Users(userid)
);

create table machine
(
	machineId int identity(1,1) primary key,
	[name] varchar(100) not null,
	gymId int,
	foreign key (gymId) references gym(gymId) 
)


create table exercise
(
	exerciseid int identity(1,1) Primary key,
	musclegroup varchar(100) not null,
	[name] varchar(100) not null,
)

create table Exercise_Machine
(
	ExerciseMachineId int identity(1,1) primary key,
	exerciseid int,
	machineid int,
	Foreign key (exerciseid) references exercise(exerciseid),
	Foreign key (machineid) references machine(machineid)	
)

CREATE TABLE Meal(
	MealName VARCHAR(100) unique not null,
	mealid int identity(1,1) primary key,
	PortionSize varchar(30),
	Fats INT,
	Proteins INT,
	Carbs INT,
	Calories INT,
);

CREATE TABLE Allergen(
	AllergenId int identity(1,1) primary key,
	AllergenName VarCHAR(100) unique,
);

CREATE TABLE Workout_Exercise
(
	WorkoutExerciseId INT identity(1,1) primary key,
	WorkoutPlanId INT,
	ExerciseId INT,
	Foreign key (WorkoutPlanId) References WorkoutPlan(WorkoutPlanId),
	Foreign Key (ExerciseId) References Exercise(ExerciseId)

);


CREATE TABLE DietPlan_Meal(
	DietPlanMealId INT identity(1,1) primary key,
	DietPlanId INT,
	MealName VARCHAR(100),
	Foreign key (DietPlanId) References DietPlan(DietPlanId),
	Foreign key (MealName) References Meal(MealName)
);


CREATE TABLE Meal_Allergen(
	MealAllergenId INT identity(1,1) primary key, 
	MealName VARCHAR(100),
	AllergenName VARCHAR(100),
	Foreign key (MealName) References Meal(MealName),
	Foreign Key (AllergenName) References Allergen(AllergenName)
);

CREATE TABLE AuditLog (
    AuditLogId INT PRIMARY KEY IDENTITY(1,1),
    ChangeType VARCHAR(10) NOT NULL,  -- 'Insert' or 'Delete'
    TableName VARCHAR(50) NOT NULL,
    ChangedId INT NOT NULL,
    ChangedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- TRIGGERS
CREATE TRIGGER trg_Users_Insert
ON users
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'users', userid
    FROM inserted;
END;

CREATE TRIGGER trg_Users_Delete ON users
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'users', userid
    FROM deleted;
END;

CREATE TRIGGER trg_users_update ON users
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'users', userid
    FROM inserted;
END;

CREATE TRIGGER trg_member_insert
ON member
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'member', memberid
    FROM inserted;
END;

CREATE TRIGGER trg_member_update
ON member
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'member', memberid
    FROM inserted;
END;

CREATE TRIGGER trg_member_delete
ON member
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'member', memberid
    FROM deleted;
END;

CREATE TRIGGER trg_owner_insert
ON owner
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'owner', ownerid
    FROM inserted;
END;

CREATE TRIGGER trg_owner_update
ON owner
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'owner', ownerid
    FROM inserted;
END;

CREATE TRIGGER trg_owner_delete
ON owner
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'owner', ownerid
    FROM deleted;
END;

CREATE TRIGGER trg_trainer_insert
ON trainer
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'trainer', trainerid
    FROM inserted;
END;

CREATE TRIGGER trg_trainer_update
ON trainer
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'trainer', trainerid
    FROM inserted;
END;

CREATE TRIGGER trg_trainer_delete
ON trainer
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'trainer', trainerid
    FROM deleted;
END;

CREATE TRIGGER trg_feedback_insert
ON feedback
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'feedback', feedbackid
    FROM inserted;
END;

CREATE TRIGGER trg_feedback_delete
ON feedback
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'feedback', feedbackid
    FROM deleted;
END;

CREATE TRIGGER trg_feedback_update
ON feedback
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'feedback', feedbackid
    FROM inserted;
END;

CREATE TRIGGER trg_trainer_gym_insert
ON trainer_gym
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'trainer_gym', trainerid
    FROM inserted;
END;

CREATE TRIGGER trg_trainer_gym_update
ON trainer_gym
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'trainer_gym', trainerid
    FROM inserted;
END;

CREATE TRIGGER trg_trainer_gym_delete
ON trainer_gym
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'trainer_gym', trainerid
    FROM deleted;
END;

CREATE TRIGGER trg_appointment_insert
ON appointment
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'appointment', appointmentid
    FROM inserted;
END;

CREATE TRIGGER trg_appointment_update
ON appointment
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'appointment', appointmentid
    FROM inserted;
END;

CREATE TRIGGER trg_appointment_delete
ON appointment
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'appointment', appointmentid
    FROM deleted;
END;

CREATE TRIGGER trg_admin_insert
ON [admin]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'admin', adminid
    FROM inserted;
END;

CREATE TRIGGER trg_admin_update
ON [admin]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'admin', adminid
    FROM inserted;
END;

CREATE TRIGGER trg_admin_delete
ON [admin]
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'admin', adminid
    FROM deleted;
END;

CREATE TRIGGER trg_workoutPlan_insert
ON workoutPlan
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'workoutPlan', workoutPlanID
    FROM inserted;
END;

CREATE TRIGGER trg_workoutPlan_update
ON workoutPlan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'workoutPlan', workoutPlanID
    FROM inserted;
END;

CREATE TRIGGER trg_workoutPlan_delete
ON workoutPlan
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'workoutPlan', workoutPlanID
    FROM deleted;
END;

CREATE TRIGGER trg_exercise_insert
ON exercise
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'exercise', exerciseid
    FROM inserted;
END;

CREATE TRIGGER trg_exercise_update
ON exercise
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'exercise', exerciseid
    FROM inserted;
END;

CREATE TRIGGER trg_exercise_delete
ON exercise
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'exercise', exerciseid
    FROM deleted;
END;

CREATE TRIGGER trg_machine_insert
ON machine
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'machine', machineId
    FROM inserted;
END;

CREATE TRIGGER trg_machine_update
ON machine
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'machine', machineId
    FROM inserted;
END;

CREATE TRIGGER trg_machine_delete
ON machine
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'machine', machineId
    FROM deleted;
END;

CREATE TRIGGER trg_dietplan_insert
ON dietplan
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'dietplan', dietplanId
    FROM inserted;
END;

CREATE TRIGGER trg_dietplan_update
ON dietplan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'dietplan', dietplanId
    FROM inserted;
END;

CREATE TRIGGER trg_dietplan_delete
ON dietplan
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'dietplan', dietplanId
    FROM deleted;
END;

CREATE TRIGGER trg_Meal_insert
ON Meal
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'Meal', mealid
    FROM inserted;
END;

CREATE TRIGGER trg_Meal_update
ON Meal
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'Meal', mealid
    FROM inserted;
END;

CREATE TRIGGER trg_Meal_delete
ON Meal
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'Meal', mealid
    FROM deleted;
END;

CREATE TRIGGER trg_Allergen_insert
ON Allergen
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'Allergen', AllergenId
    FROM inserted;
END;

CREATE TRIGGER trg_Allergen_update
ON Allergen
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'Allergen', AllergenId
    FROM inserted;
END;

CREATE TRIGGER trg_Allergen_delete
ON Allergen
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'Allergen', AllergenId
    FROM deleted;
END;

CREATE TRIGGER trg_GYM_insert
ON GYM
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'GYM', GymId
    FROM inserted;
END;

CREATE TRIGGER trg_GYM_update
ON GYM
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'GYM', GymId
    FROM inserted;
END;

CREATE TRIGGER trg_GYM_delete
ON GYM
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'GYM', GymId
    FROM deleted;
END;

CREATE TRIGGER trg_Workout_Exercise_insert
ON Workout_Exercise
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'Workout_Exercise', WorkoutExerciseId
    FROM inserted;
END;

CREATE TRIGGER trg_Workout_Exercise_update
ON Workout_Exercise
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'Workout_Exercise', WorkoutExerciseId
    FROM inserted;
END;

CREATE TRIGGER trg_Workout_Exercise_delete
ON Workout_Exercise
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'Workout_Exercise', WorkoutExerciseId
    FROM deleted;
END;

CREATE TRIGGER trg_DietPlan_Meal_insert
ON DietPlan_Meal
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'DietPlan_Meal', DietPlanMealId
    FROM inserted;
END;

CREATE TRIGGER trg_DietPlan_Meal_update
ON DietPlan_Meal
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'DietPlan_Meal', DietPlanMealId
    FROM inserted;
END;

CREATE TRIGGER trg_DietPlan_Meal_delete
ON DietPlan_Meal
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'DietPlan_Meal', DietPlanMealId
    FROM deleted;
END;

CREATE TRIGGER trg_Meal_Allergen_insert
ON Meal_Allergen
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'Meal_Allergen', MealAllergenId
    FROM inserted;
END;

CREATE TRIGGER trg_Meal_Allergen_update
ON Meal_Allergen
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'Meal_Allergen', MealAllergenId
    FROM inserted;
END;

CREATE TRIGGER trg_Meal_Allergen_delete
ON Meal_Allergen
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'Meal_Allergen', MealAllergenId
    FROM deleted;
END;

CREATE TRIGGER trg_Execise_Machine_insert
ON Exercise_Machine
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Insert', 'Exercise_Machine', ExerciseMachineId
    FROM inserted;
END;

CREATE TRIGGER trg_Exercise_Machine_update
ON Exercise_Machine
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Update', 'Exercise_Machine', ExerciseMachineId
    FROM inserted;
END;

CREATE TRIGGER trg_Exercise_Machine_delete
ON Exercise_Machine
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditLog (ChangeType, TableName, ChangedId)
    SELECT 'Delete', 'Exercise_Machine', ExerciseMachineId
    FROM deleted;
END;

CREATE PROCEDURE SoftDeleteUser
(
    @userid int
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE users
    SET isActive = 0
    WHERE userid = @userid;
END;


create PROCEDURE InsertMember
(
    @username varchar(255),
    @password varchar(255),
    @fname varchar(255),
    @lname varchar(255),
    @contact varchar(30),
    @dietplanid int,
    @workoutplanid int,
    @membership varchar(40),
	@gymName varchar(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userid int;

 
    INSERT INTO users (username, password, fname, lname, contact, userType, isActive)
    VALUES (@username, @password, @fname, @lname, @contact, 'member', 1);

    -- Get the userid of the newly inserted user
    SET @userid = SCOPE_IDENTITY();

    -- Insert into member table
    INSERT INTO member (memberid, dietplanid, workoutplanid, membership, gymName)
    VALUES (@userid, @dietplanid, @workoutplanid, @membership, @gymName);
	update gym set gym.Members = gym.Members + 1 where GYM.gymName = @gymName
END;

CREATE PROCEDURE InsertNonMember
(
    @username varchar(255),
    @password varchar(255),
    @fname varchar(255),
    @lname varchar(255),
    @contact varchar(30),
    @userType varchar(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @userid int;

  
    INSERT INTO users (username, password, fname, lname, contact, userType, isActive)
    VALUES (@username, @password, @fname, @lname, @contact, @userType, 1);

  
    SET @userid = SCOPE_IDENTITY();

  
    IF @userType = 'trainer'
    BEGIN
        INSERT INTO trainer (trainerid)
        VALUES (@userid);
    END
    ELSE IF @userType = 'admin'
    BEGIN
        INSERT INTO [admin] (adminid)
        VALUES (@userid);
    END
    ELSE IF @userType = 'owner'
    BEGIN
        INSERT INTO owner (ownerid)
        VALUES (@userid);
    END
END;

CREATE PROCEDURE SoftDeleteUsersByGym
    @GymName VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

   
    DECLARE @GymId INT;
    SELECT @GymId = GymId FROM GYM WHERE gymName = @GymName;

    IF @GymId IS NOT NULL
    BEGIN
     
        UPDATE users
        SET isActive = 0
        WHERE userid IN (
            SELECT memberid
            FROM member
            WHERE gymName = @GymName
        );

        PRINT 'Users associated with the gym have been soft-deleted.';
    END
    ELSE
    BEGIN
        PRINT 'Gym does not exist.';
    END
END;

CREATE PROCEDURE RemoveGym
    @GymName VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    
    DECLARE @GymId INT;
    SELECT @GymId = GymId FROM GYM WHERE gymName = @GymName;

    IF @GymId IS NOT NULL
    BEGIN
        
        EXEC SoftDeleteUsersByGym @GymName;

        
        DELETE FROM trainer_gym WHERE gymid = @GymId;

        
        update GYM set isApproved = 0 WHERE gymName = @GymName;

        PRINT 'Gym removed successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Gym does not exist.';
    END
END;

EXEC InsertNonMember 'oliver_owner', 'owner_pass', 'Oliver', 'Owner', '+1234567890', 'owner';
EXEC InsertNonMember 'ava_owner', 'owner_pass', 'Ava', 'Owner', '+1987654321', 'owner';
EXEC InsertNonMember 'noah_owner', 'owner_pass', 'Noah', 'Owner', '+1122334455', 'owner';
EXEC InsertNonMember 'mia_owner', 'owner_pass', 'Mia', 'Owner', '+1555666777', 'owner';
EXEC InsertNonMember 'jacob_owner', 'owner_pass', 'Jacob', 'Owner', '+1444333222', 'owner';
EXEC InsertNonMember 'emma_admin', 'admin_pass', 'Emma', 'Admin', '+1666999888', 'admin';
EXEC InsertNonMember 'william_admin', 'admin_pass', 'William', 'Admin', '+1777888999', 'admin';
EXEC InsertNonMember 'sophia_admin', 'admin_pass', 'Sophia', 'Admin', '+1888999000', 'admin';
EXEC InsertNonMember 'david_admin', 'admin_pass', 'David', 'Admin', '+1999000111', 'admin';
EXEC InsertNonMember 'olivia_admin', 'admin_pass', 'Olivia', 'Admin', '+1222333444', 'admin';
EXEC InsertNonMember 'lucas_trainer', 'trainer_pass', 'Lucas', 'Trainer', '+1333444555', 'trainer';
EXEC InsertNonMember 'amelia_trainer', 'trainer_pass', 'Amelia', 'Trainer', '+1444555666', 'trainer';
EXEC InsertNonMember 'logan_trainer', 'trainer_pass', 'Logan', 'Trainer', '+1555666777', 'trainer';
EXEC InsertNonMember 'mia_trainer', 'trainer_pass', 'Mia', 'Trainer', '+1666777888', 'trainer';
EXEC InsertNonMember 'ethan_trainer', 'trainer_pass', 'Ethan', 'Trainer', '+1777888999', 'trainer';

-- Inserting into gym table
INSERT INTO gym ( gymName, [Location], Members, isApproved, AdminID, OwnerID)
VALUES 
( 'FitLife Gym1', '123 Main St', 100, 1, 6, 1),
( 'Powerhouse Fitness1', '456 Elm St', 150, 1, 7, 2),
( 'IronWorks Gym1', '789 Oak St', 200, 1, 8, 3),
( 'FlexZone1', '101 Maple St', 120, 1, 9, 4),
( 'BodyTech1', '202 Pine St', 80, 1, 10, 5),
( 'HealthHub1', '303 Cedar St', 90, 1, 6, 5);

-- Inserting into dietplan table
INSERT INTO dietplan ( dietplanName, nutriValue, [type], purpose)
VALUES 
( 'Weight Loss', 1500, 'Low Carb', 'Weight Loss'),
( 'Muscle Gain', 2500, 'High Protein', 'Muscle Gain'),
( 'Maintenance', 2000, 'Balanced', 'Maintain Weight'),
( 'Keto', 1800, 'Ketogenic', 'Ketosis'),
( 'Vegan', 1600, 'PlantBased', 'Vegan Diet'),
( 'Paleo', 1900, 'Paleolithic', 'Paleo Diet');


-- Inserting into workoutPlan table
INSERT INTO workoutPlan (workoutplanName, [sets], reps)
VALUES 
( 'Beginner Full Body', 3, 12),
( 'Intermediate Split', 4, 10),
( 'Advanced Push/Pull', 5, 8),
( 'Cardiovascular', 0, 0),
( 'Strength Training', 6, 6),
( 'Circuit Training', 3, 15);

EXEC InsertMember 'alice_smith', 'password123', 'Alice', 'Smith', '+1234567890', 1, 1, 'Gold', 'FitLife Gym';
EXEC InsertMember 'bob_jones', 'pass123', 'Bob', 'Jones', '+1987654321', 2,2, 'Silver', 'Powerhouse Fitness';
EXEC InsertMember 'emily_williams', 'emily_pass', 'Emily', 'Williams', '+1122334455', 3, 1, 'Platinum', 'IronWorks Gym';
EXEC InsertMember 'michael_taylor', 'mike_pass', 'Michael', 'Taylor', '+1555666777', 4, 3, 'Gold', 'FlexZone';
EXEC InsertMember 'sarah_davis', 'sarah123', 'Sarah', 'Davis', '+1444333222', 4, 5, 'Silver', 'BodyTech';
EXEC InsertMember 'david_wilson', 'david_pass', 'David', 'Wilson', '+1666999888', 6, 6, 'Gold', 'HealthHub';
EXEC InsertMember 'olivia_miller', 'olivia_pass', 'Olivia', 'Miller', '+1777888999', 1, 5, 'Silver', 'FitLife Gym';
EXEC InsertMember 'jacob_brown', 'jacob_pass', 'Jacob', 'Brown', '+1888999000', 1, 2, 'Platinum', 'Powerhouse Fitness';
EXEC InsertMember 'ava_johnson', 'ava_pass', 'Ava', 'Johnson', '+1999000111', 5, 3, 'Gold', 'IronWorks Gym';
EXEC InsertMember 'ethan_anderson', 'ethan_pass', 'Ethan', 'Anderson', '+1222333444', 4, 3, 'Silver', 'FlexZone';
EXEC InsertMember 'mia_torres', 'mia_pass', 'Mia', 'Torres', '+1333444555', 2, 5, 'Platinum', 'BodyTech';
EXEC InsertMember 'noah_garcia', 'noah_pass', 'Noah', 'Garcia', '+1444555666', 4, 6, 'Gold', 'HealthHub';
EXEC InsertMember 'emma_hernandez', 'emma_pass', 'Emma', 'Hernandez', '+1555666777', 3, 2, 'Silver', 'FitLife Gym';
EXEC InsertMember 'william_martinez', 'william_pass', 'William', 'Martinez', '+1666777888', 4, 4, 'Gold', 'Powerhouse Fitness';

-- Inserting into feedback table
INSERT INTO feedback ( memberid, trainerid, dateandtime, rating, review)
VALUES 
( 1, 1, GETDATE(), 5, 'Great trainer, highly recommend!'),	
( 2, 2, GETDATE(), 4, 'Helped me achieve my fitness goals.'),
( 3, 3, GETDATE(), 3, 'Good experience overall.'),
( 4, 4, GETDATE(), 5, 'Very knowledgeable and friendly.'),
( 5, 5, GETDATE(), 2, 'Could improve communication with clients.'),
( 6, 6, GETDATE(), 4, 'Structured workouts, good results.');

-- Inserting into machine table
INSERT INTO machine ( [name], gymId)
VALUES 
( 'Treadmill', 1),
( 'Treadmill', 2),
( 'Treadmill', 3),
( 'Treadmill', 4),
( 'Treadmill', 5),
( 'Treadmill', 6),
( 'Dumbbell Rack', 1),
( 'Dumbbell Rack', 2),
( 'Dumbbell Rack', 3),
( 'Dumbbell Rack', 4),
( 'Dumbbell Rack', 5),
( 'Dumbbell Rack', 6),
( 'Bench Press', 1),
( 'Bench Press', 2),
( 'Bench Press', 3),
( 'Bench Press', 4),
( 'Bench Press', 5),
( 'Bench Press', 6),
( 'Leg Press', 1),
( 'Leg Press', 2),
( 'Leg Press', 3),
( 'Leg Press', 4),
( 'Leg Press', 5),
( 'Leg Press', 6),
( 'Elliptical Machine', 1),
( 'Elliptical Machine', 2),
( 'Elliptical Machine', 3),
( 'Elliptical Machine', 4),
( 'Elliptical Machine', 5),
( 'Elliptical Machine', 6),
( 'Cable Machine', 1),
( 'Cable Machine', 2),
( 'Cable Machine', 3),
( 'Cable Machine', 4),
( 'Cable Machine', 5),
( 'Cable Machine', 6),
( 'Stationary Bike', 1),
( 'Rowing Machine', 2),
( 'Smith Machine', 3),
( 'Leg Extension Machine', 4),
( 'Chest Press Machine', 5),
( 'Lat Pulldown Machine', 6)

-- Inserting into exercise table
INSERT INTO exercise ( musclegroup, [name])
VALUES 
( 'Legs', 'Squat'),
( 'Chest', 'Bench Press'),
('Back', 'Deadlift'),
( 'Arms', 'Bicep Curl'),
( 'Shoulders', 'Shoulder Press'),
( 'Abs', 'Crunches');

-- Inserting into trainer_gym table
insert into trainer_gym (trainerid, gymid, startdate) values 
(11,1, getdate()),
(12,2, getdate()),
(13,3, getdate()),
(14,4, getdate()),
(15,5, getdate());

-- Inserting into appointment table
INSERT INTO appointment ( trainerid, memberid, isAccepted, date, status)
VALUES 
( 1, 1, 1, GETDATE(), 'Confirmed'),
( 2, 2, 1, GETDATE(), 'Confirmed'),
( 3, 3, 0, GETDATE(), 'Pending'),
( 4, 4, 1, GETDATE(), 'Rejected'),
( 5, 5, 0, GETDATE(), 'Pending'),
( 6, 6, 1, GETDATE(), 'Confirmed');

-- Inserting into Meal table
INSERT INTO Meal (MealName, PortionSize, Fats, Proteins, Carbs, Calories)
VALUES 
('Breakfast', '300g', 20, 30, 40, 400),
('Lunch', '400g', 25, 35, 45, 500),
('Dinner', '350g', 30, 40, 50, 600),
('Snack', '100g', 10, 15, 20, 200),
('Pre-Workout', '150g', 15, 20, 25, 250),
('Post-Workout', '200g', 18, 25, 30, 300);

-- Inserting into Allergen table
INSERT INTO Allergen (AllergenName)
VALUES 
('Gluten'),
('Dairy'),
('Peanuts'),
('Shellfish'),
('Soy'),
('Tree Nuts');


-- Inserting into Workout_Exercise table
INSERT INTO Workout_Exercise (WorkoutPlanId, ExerciseId)
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);


-- Inserting into DietPlan_Meal table
INSERT INTO DietPlan_Meal (DietPlanId, MealName)
VALUES 
(1, 'Breakfast'),
(1, 'Lunch'),
(1, 'Dinner'),
(1, 'Snack'),
(1, 'Pre-Workout'),
(1, 'Post-Workout'),
(2, 'Breakfast'),
(2, 'Lunch'),
(2, 'Dinner'),
(2, 'Snack'),
(3, 'Pre-Workout'),
(3, 'Post-Workout'),
(3, 'Breakfast'),
(3, 'Lunch'),
(3, 'Dinner'),
(4, 'Snack'),
(4, 'Pre-Workout'),
(4, 'Post-Workout'),
(4, 'Breakfast'),
(4, 'Lunch'),
(4, 'Dinner'),
(5, 'Snack'),
(5, 'Post-Workout'),
(5, 'Breakfast'),
(5, 'Lunch'),
(6, 'Pre-Workout'),
(6, 'Breakfast'),
(6, 'Lunch'),
(6, 'Dinner')

-- Inserting into Meal_Allergen table
INSERT INTO Meal_Allergen (MealName, AllergenName)
VALUES 
('Breakfast', 'Dairy'),
('Lunch', 'Gluten'),
('Dinner', 'Shellfish'),
('Snack', 'Soy'),
('Pre-Workout', 'Tree Nuts'),
('Post-Workout', 'Peanuts');

INSERT INTO Exercise_Machine (exerciseid, machineid)
VALUES
    (1, 19),  -- Squat - Leg Press
    (2, 13),  -- Bench Press - Bench Press
    (3, 39),  -- Deadlift - Smith Machine
    (4, 7),   -- Bicep Curl - Dumbbell Rack
    (5, 42),  -- Shoulder Press - Lat Pulldown Machine
    (6, NULL); -- Abs - No specific machine assigned

INSERT INTO trainer_request (trainerid, gymid, status)
VALUES
    (1, 1, 'Pending'),
    (2, 2, 'Approved'),
    (3, 3, 'Rejected');

insert into trainer_request (trainerid, gymid, status) values
(1,	1,	'Pending'),
(2	,2,	'Approved'),
(3	,3,	'Rejected'),
(14	,4,	'Pending'),
(14	,5,	'Pending'),
(13	,4,	'Pending'),
(14	,1,	'Pending')

















-- Inserting into users table
EXEC InsertNonMember 'emma_adams', 'emma_pass', 'Emma', 'Adams', '+1333999888', 'admin';
EXEC InsertNonMember 'william_clark', 'william_pass', 'William', 'Clark', '+1444222111', 'admin';
EXEC InsertNonMember 'sophia_hall', 'sophia_pass', 'Sophia', 'Hall', '+1555333222', 'admin';
EXEC InsertNonMember 'david_lewis', 'david_pass', 'David', 'Lewis', '+1666444333', 'admin';
EXEC InsertNonMember 'olivia_nelson', 'olivia_pass', 'Olivia', 'Nelson', '+1777555444', 'admin';

EXEC InsertNonMember 'liam_smith', 'liam_pass', 'Liam', 'Smith', '+1888666555', 'trainer';
EXEC InsertNonMember 'william_brown', 'william_pass', 'William', 'Brown', '+1222888777', 'trainer';
EXEC InsertNonMember 'isabella_taylor', 'isabella_pass', 'Isabella', 'Taylor', '+1333999888', 'trainer';
EXEC InsertNonMember 'mason_davis', 'mason_pass', 'Mason', 'Davis', '+1444000999', 'trainer';


-- Inserting into gym table
INSERT INTO gym (gymName, [Location], Members, isApproved, AdminID, OwnerID)
VALUES 
('Fit & Flex', '123 Oak St', 120, 1, 6, 1),
('Gym Central', '456 Maple St', 180, 1, 7, 2),
('Ultimate Fitness', '789 Elm St', 220, 8, 14, 3),
('Body Works', '101 Pine St', 140, 1, 9, 4),
('Core Fitness', '202 Cedar St', 100, 1, 10, 5),
('Vitality Gym', '303 Walnut St', 110, 1, 7, 2);

-- Inserting into dietplan table
INSERT INTO dietplan (dietplanName, nutriValue, [type], purpose)
VALUES 
('Weight Maintenance', 2000, 'Balanced', 'Maintain Weight'),
('Ketogenic', 1800, 'Low Carb', 'Ketosis'),
('High Protein', 2200, 'High Protein', 'Muscle Gain'),
('Low Calorie', 1500, 'Low Calorie', 'Weight Loss');

-- Inserting into feedback table
INSERT INTO feedback (memberid, trainerid, dateandtime, rating, review)
VALUES 
(1, 16, GETDATE(), 5, 'Excellent trainer, helped me achieve my goals.'),
(2, 17, GETDATE(), 4, 'Knowledgeable and supportive.'),
(3, 18, GETDATE(), 3, 'Good workout routines.'),
(4, 19, GETDATE(), 5, 'Friendly and motivating.'),
(5, 20, GETDATE(), 2, 'Could improve communication.'),
(6, 21, GETDATE(), 4, 'Great results, highly recommend.');

-- Inserting into machine table
INSERT INTO machine ([name], gymId)
VALUES 
('Leg Press', 7),
('Chest Press Machine', 8),
('Lat Pulldown Machine', 9),
('Leg Extension Machine', 10),
('Cable Machine', 11),
('Smith Machine', 12);

-- Inserting into exercise table
INSERT INTO exercise (musclegroup, [name])
VALUES 
('Legs', 'Leg Press'),
('Chest', 'Chest Press'),
('Back', 'Lat Pulldown'),
('Legs', 'Leg Extension'),
('Arms', 'Bicep Curl'),
('Legs', 'Squats');

-- Inserting into trainer_gym table
INSERT INTO trainer_gym (trainerid, gymid, startdate)
VALUES 
(16, 1, GETDATE()),
(17, 2, GETDATE()),
(18, 3, GETDATE()),
(19, 4, GETDATE()),
(20, 5, GETDATE()),
(21, 6, GETDATE());

-- Inserting into appointment table
INSERT INTO appointment (trainerid, memberid, isAccepted, date, status)
VALUES 
(16, 1, 1, GETDATE(), 'Confirmed'),
(17, 2, 1, GETDATE(), 'Confirmed'),
(18, 3, 0, GETDATE(), 'Pending'),
(19, 4, 1, GETDATE(), 'Rejected'),
(20, 5, 0, GETDATE(), 'Pending'),
(21, 6, 1, GETDATE(), 'Confirmed');

-- Inserting into Meal_Allergen table
INSERT INTO Meal_Allergen (MealName, AllergenName)
VALUES 
('Breakfast', 'Dairy'),
('Lunch', 'Gluten'),
('Dinner', 'Shellfish'),
('Snack', 'Soy'),
('Pre-Workout', 'Tree Nuts'),
('Post-Workout', 'Peanuts');

-- Inserting into Exercise_Machine table
INSERT INTO Exercise_Machine (exerciseid, machineid)
VALUES 
(1, 1),  -- Leg Press - Leg Press
(2, 2),  -- Chest Press - Chest Press Machine
(3, 3),  -- Lat Pulldown - Lat Pulldown Machine
(4, 4),  -- Leg Extension - Leg Extension Machine
(5, 5),  -- Bicep Curl - Cable Machine
(6, 6);  -- Squats - Smith Machine

-- Inserting into trainer_request table
INSERT INTO trainer_request (trainerid, gymid, status)
VALUES 
(16, 1, 'Pending'),
(17, 2, 'Approved'),
(18, 3, 'Rejected'),
(19, 4, 'Pending'),
(20, 5, 'Pending'),
(21, 6, 'Pending');
