USE MinionsDB

CREATE TABLE Towns
(
Id INT IDENTITY,
Name VARCHAR(50) NOT NULL,
Country VARCHAR(50) NOT NULL,
CONSTRAINT PK_Towns PRIMARY KEY (Id)
)

CREATE TABLE Minions
(
Id INT IDENTITY,
Name VARCHAR(50) NOT NULL,
Age INT,
TownId INT,
CONSTRAINT PK_Minions PRIMARY KEY (Id),
CONSTRAINT FK_Minions_Towns
FOREIGN KEY (TownId)
REFERENCES Towns (Id)
)

CREATE TABLE Villains
(
Id INT IDENTITY,
Name VARCHAR(50) NOT NULL,
Evilness VARCHAR(10) NOT NULL,
CONSTRAINT PK_Villains PRIMARY KEY (Id)
)

CREATE TABLE MinionsVillains
(
MinionId INT,
VillainId INT,
CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId),
CONSTRAINT FK_MinionsVillains_Minions
FOREIGN KEY (MinionId)
REFERENCES Minions (Id),
CONSTRAINT FK_MinionsVillains_Villains
FOREIGN KEY (VillainId)
REFERENCES Villains (Id)
)

INSERT INTO Towns (Name, Country)
VALUES 
('Sofia', 'Bulgaria'),
('Paris', 'France'),
('Rome', 'Italy'),
('Madrid', 'Spain'),
('Frankfurt', 'Germany')

INSERT INTO Minions (Name, Age, TownId)
VALUES 
('Bob', 10, 2),
('Stuart', 13, 4),
('Kevin', 15, 5),
('Carl', 11, 3),
('Jerry', 13, 1)

INSERT INTO Villains (Name, Evilness)
VALUES
('Scarlet', 'super evil'),
('Felonius', 'good'),
('Vector', 'bad'),
('Balthazar', 'evil'),
('Mr. Perkins', 'super evil')

INSERT INTO MinionsVillains (MinionId, VillainId)
VALUES
(1, 5),
(2, 4),
(3, 1),
(4, 2),
(5, 3)

