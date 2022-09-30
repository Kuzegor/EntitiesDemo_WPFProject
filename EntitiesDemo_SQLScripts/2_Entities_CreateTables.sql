use Entities;

create table EntityTypes(
id int identity primary key,
TypeName nvarchar(100));

create table Entities(
id int identity primary key,
EntityName nvarchar(100),
EntityTypeId int references EntityTypes(id) on delete set null on update cascade,
EntityPrice decimal,
EntityDescription nvarchar(max));