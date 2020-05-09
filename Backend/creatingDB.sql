-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-04-29 20:46:30.204

-- tables
-- Table: Meeting
CREATE TABLE Meeting (
    IdMeeting int  NOT NULL,
    Title nvarchar(255)  NOT NULL,
    Date date  NOT NULL,
    Time time  NOT NULL,
    Length int  NOT NULL,
    Location nvarchar(255)  NOT NULL,
    Description text  NULL,
    Project int  NOT NULL,
    CONSTRAINT Meeting_pk PRIMARY KEY  (IdMeeting)
);

-- Table: Meeting_attendence
CREATE TABLE Meeting_attendence (
    IdAttendence int  NOT NULL,
    Attendence bit  NOT NULL,
    Meeting int  NOT NULL,
    "User" int  NOT NULL,
    CONSTRAINT Meeting_attendence_pk PRIMARY KEY  (IdAttendence)
);

-- Table: Message
CREATE TABLE Message (
    IdMessage int  NOT NULL,
    Receiver int  NOT NULL,
    Sender int  NOT NULL,
    Message text  NOT NULL,
    Attachment nvarchar(255)  NULL,
    CONSTRAINT Message_pk PRIMARY KEY  (IdMessage)
);

-- Table: Milestone
CREATE TABLE Milestone (
    IdMilestone int  NOT NULL,
    Title nvarchar(255)  NOT NULL,
    Description text  NOT NULL,
    Date date  NOT NULL,
    Project int  NOT NULL,
    CONSTRAINT Milestone_pk PRIMARY KEY  (IdMilestone)
);

-- Table: Note
CREATE TABLE Note (
    IdNote int  NOT NULL,
    Title nvarchar(255)  NOT NULL,
    Subject nvarchar(255)  NOT NULL,
    Author int  NOT NULL,
    Note text  NOT NULL,
    Meeting int  NOT NULL,
    Attachments nvarchar(255)  NULL,
    CONSTRAINT Note_pk PRIMARY KEY  (IdNote)
);

-- Table: Notification
CREATE TABLE Notification (
    IdNotification int  NOT NULL,
    Notification nvarchar(255)  NOT NULL,
    "User" int  NOT NULL,
    CONSTRAINT Notification_pk PRIMARY KEY  (IdNotification)
);

-- Table: Personal_Note
CREATE TABLE Personal_Note (
    IdNote int  NOT NULL,
    Title nvarchar(255)  NOT NULL,
    Description text  NOT NULL,
    "User" int  NOT NULL,
    CONSTRAINT Personal_Note_pk PRIMARY KEY  (IdNote)
);

-- Table: Phase
CREATE TABLE Phase (
    IdPhase int  NOT NULL,
    Name nvarchar(255)  NOT NULL,
    StartDate date  NOT NULL,
    EndDate date  NOT NULL,
    Project_IdProject int  NOT NULL,
    CONSTRAINT Phase_pk PRIMARY KEY  (IdPhase)
);

-- Table: Post
CREATE TABLE Post (
    IdPost int  NOT NULL,
    Content text  NOT NULL,
    DateOfPublication date  NOT NULL,
    Writer int  NOT NULL,
    Project int  NULL,
    Attachment nvarchar(255)  NULL,
    CONSTRAINT Post_pk PRIMARY KEY  (IdPost)
);

-- Table: Profile
CREATE TABLE Profile (
    IdProfile int  NOT NULL,
    Major nvarchar(250)  NULL,
    Skills text  NOT NULL,
    Experiences text  NOT NULL,
    Semester int  NULL,
    "User" int  NOT NULL,
    CONSTRAINT Profile_pk PRIMARY KEY  (IdProfile)
);

-- Table: Project
CREATE TABLE Project (
    IdProject int  NOT NULL,
    Name nvarchar(max)  NOT NULL,
    Description text  NOT NULL,
    StartDate date  NOT NULL,
    EndDate date  NOT NULL,
    Superviser int  NOT NULL,
    Icon nvarchar(max)  NOT NULL,
    Status int  NOT NULL,
    CONSTRAINT Project_pk PRIMARY KEY  (IdProject)
);

-- Table: Project_History
CREATE TABLE Project_History (
    IdHistory int  NOT NULL,
    Date date  NOT NULL,
    Change text  NOT NULL,
    Project int  NOT NULL,
    Who_Change int  NOT NULL,
    CONSTRAINT Project_History_pk PRIMARY KEY  (IdHistory)
);

-- Table: Project_Members
CREATE TABLE Project_Members (
    IdProject_Member int  NOT NULL,
    Project int  NOT NULL,
    Role nvarchar(max)  NOT NULL,
    Member int  NOT NULL,
    CONSTRAINT Project_Members_pk PRIMARY KEY  (IdProject_Member)
);

-- Table: Project_Promoter
CREATE TABLE Project_Promoter (
    IdProject_Promoter int  NOT NULL,
    Project int  NOT NULL,
    "User" int  NOT NULL,
    CONSTRAINT Project_Promoter_pk PRIMARY KEY  (IdProject_Promoter)
);

-- Table: Project_Status
CREATE TABLE Project_Status (
    idStatus int  NOT NULL,
    Name nvarchar(max)  NOT NULL,
    CONSTRAINT Project_Status_pk PRIMARY KEY  (idStatus)
);

-- Table: Task
CREATE TABLE Task (
    IdTask int  NOT NULL,
    Title nvarchar(255)  NOT NULL,
    Description text  NOT NULL,
    ExpectedEndDate date  NOT NULL,
    ActualEndDate date  NOT NULL,
    StartDate date  NOT NULL,
    Status nvarchar(255)  NOT NULL,
    Project int  NOT NULL,
    Creator int  NOT NULL,
    CONSTRAINT Task_pk PRIMARY KEY  (IdTask)
);

-- Table: Task_Assigning
CREATE TABLE Task_Assigning (
    IdAssign int  NOT NULL,
    Task int  NOT NULL,
    "User" int  NOT NULL,
    CONSTRAINT Task_Assigning_pk PRIMARY KEY  (IdAssign)
);

-- Table: URL
CREATE TABLE URL (
    IdURL int  NOT NULL,
    Link nvarchar(250)  NOT NULL,
    Project int  NOT NULL,
    Category nvarchar(255)  NOT NULL,
    CONSTRAINT URL_pk PRIMARY KEY  (IdURL)
);

-- Table: User
CREATE TABLE "User" (
    IdUser int  NOT NULL,
    Email nvarchar(255)  NOT NULL,
    Password nvarchar(255)  NOT NULL,
    SALT nvarchar(max)  NOT NULL,
    LastName nvarchar(255)  NOT NULL,
    FirstName nvarchar(255)  NOT NULL,
    CONSTRAINT User_pk PRIMARY KEY  (IdUser)
);

-- foreign keys
-- Reference: Meeting_Project (table: Meeting)
ALTER TABLE Meeting ADD CONSTRAINT Meeting_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Meeting_attendence_Meeting (table: Meeting_attendence)
ALTER TABLE Meeting_attendence ADD CONSTRAINT Meeting_attendence_Meeting
    FOREIGN KEY (Meeting)
    REFERENCES Meeting (IdMeeting);

-- Reference: Meeting_attendence_User (table: Meeting_attendence)
ALTER TABLE Meeting_attendence ADD CONSTRAINT Meeting_attendence_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Milestone_Project (table: Milestone)
ALTER TABLE Milestone ADD CONSTRAINT Milestone_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Note_Meeting (table: Note)
ALTER TABLE Note ADD CONSTRAINT Note_Meeting
    FOREIGN KEY (Meeting)
    REFERENCES Meeting (IdMeeting);

-- Reference: Note_User (table: Note)
ALTER TABLE Note ADD CONSTRAINT Note_User
    FOREIGN KEY (Author)
    REFERENCES "User" (IdUser);

-- Reference: Notification_User (table: Notification)
ALTER TABLE Notification ADD CONSTRAINT Notification_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Personal_Note_User (table: Personal_Note)
ALTER TABLE Personal_Note ADD CONSTRAINT Personal_Note_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Post_Project (table: Post)
ALTER TABLE Post ADD CONSTRAINT Post_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Post_User (table: Post)
ALTER TABLE Post ADD CONSTRAINT Post_User
    FOREIGN KEY (Writer)
    REFERENCES "User" (IdUser);

-- Reference: Profile_User (table: Profile)
ALTER TABLE Profile ADD CONSTRAINT Profile_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Project_History_Project (table: Project_History)
ALTER TABLE Project_History ADD CONSTRAINT Project_History_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Project_History_User (table: Project_History)
ALTER TABLE Project_History ADD CONSTRAINT Project_History_User
    FOREIGN KEY (Who_Change)
    REFERENCES "User" (IdUser);

-- Reference: Project_ProjectMember_Project (table: Project_Members)
ALTER TABLE Project_Members ADD CONSTRAINT Project_ProjectMember_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Project_ProjectMember_User (table: Project_Members)
ALTER TABLE Project_Members ADD CONSTRAINT Project_ProjectMember_User
    FOREIGN KEY (Member)
    REFERENCES "User" (IdUser);

-- Reference: Project_Project_Status (table: Project)
ALTER TABLE Project ADD CONSTRAINT Project_Project_Status
    FOREIGN KEY (Status)
    REFERENCES Project_Status (idStatus);

-- Reference: Project_Promoter_Project (table: Project_Promoter)
ALTER TABLE Project_Promoter ADD CONSTRAINT Project_Promoter_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Project_Promoter_User (table: Project_Promoter)
ALTER TABLE Project_Promoter ADD CONSTRAINT Project_Promoter_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Project_User (table: Project)
ALTER TABLE Project ADD CONSTRAINT Project_User
    FOREIGN KEY (Superviser)
    REFERENCES "User" (IdUser);

-- Reference: Receiver_User (table: Message)
ALTER TABLE Message ADD CONSTRAINT Receiver_User
    FOREIGN KEY (Receiver)
    REFERENCES "User" (IdUser);

-- Reference: Sender_User (table: Message)
ALTER TABLE Message ADD CONSTRAINT Sender_User
    FOREIGN KEY (Sender)
    REFERENCES "User" (IdUser);

-- Reference: Step_Project (table: Phase)
ALTER TABLE Phase ADD CONSTRAINT Step_Project
    FOREIGN KEY (Project_IdProject)
    REFERENCES Project (IdProject);

-- Reference: Task_Assigning_Task (table: Task_Assigning)
ALTER TABLE Task_Assigning ADD CONSTRAINT Task_Assigning_Task
    FOREIGN KEY (Task)
    REFERENCES Task (IdTask);

-- Reference: Task_Assigning_User (table: Task_Assigning)
ALTER TABLE Task_Assigning ADD CONSTRAINT Task_Assigning_User
    FOREIGN KEY ("User")
    REFERENCES "User" (IdUser);

-- Reference: Task_Project (table: Task)
ALTER TABLE Task ADD CONSTRAINT Task_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- Reference: Task_User (table: Task)
ALTER TABLE Task ADD CONSTRAINT Task_User
    FOREIGN KEY (Creator)
    REFERENCES "User" (IdUser);

-- Reference: URL_Project (table: URL)
ALTER TABLE URL ADD CONSTRAINT URL_Project
    FOREIGN KEY (Project)
    REFERENCES Project (IdProject);

-- End of file.

