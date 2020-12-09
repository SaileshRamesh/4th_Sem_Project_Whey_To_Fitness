CREATE TABLE [dbo].[login] (
    [name]     VARCHAR (50) NOT NULL,
    [dob]      DATE         NOT NULL,
    [email_id] VARCHAR (50) NOT NULL,
    [password] VARCHAR (50) NOT NULL, 
    [gender] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_login] PRIMARY KEY ([name])
);

