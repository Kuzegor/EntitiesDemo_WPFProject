use Entities;

insert into dbo.EntityTypes(TypeName)
values ('Type A'),('Type B'),('Type C'),('Type D'),
('Type E'),('Type F'),('Type G'),('Type H'),('Type I'),('Type J'),
('Type K'),('Type L'),('Type M'),('Type N'),('Type O'),('Type P'),('Type Q'),
('Type R'),('Type S'),('Type T'),('Type U'),
('Type V'),('Type W'),('Type X'),('Type Y'),('Type Z');

insert into dbo.Entities(EntityName,EntityTypeId)
values ('Product #1',1),('Product #2',1),
('Product #3',1),('Product #4',1),
('Product #5',1),('Product #6',1),
('Product #7',1),('Product #8',1),
('Product #9',1),('Product #10',1),
('Product #11',2),('Product #12',2),
('Product #13',1),('Product #14',1),
('Product #15',3),('Product #16',3),
('Product #17',3),('Product #18',3),
('Product #19',3),('Product #20',4),
('Product #21',4),('Product #22',4),
('Product #23',4),('Product #24',4),
('Product #25',4),('Product #26',4),
('Product #27',4),('Product #28',4),
('Product #29',4),('Product #30',4),
('Product #31',4),('Product #32',4),
('Product #33',4),('Product #34',4),
('Product #35',4),('Product #36',4),
('Product #37',4),('Product #38',4),
('Product #39',4),('Product #40',4),
('Product #41',4),('Product #42',4),
('Product #43',4),('Product #44',4),
('Product #45',4),('Product #46',4),
('Product #47',4),('Product #48',4),
('Product #49',4),('Product #50',4),
('Product #51',4),('Product #52',4),
('Product #53',4),('Product #54',4),
('Product of type E',5),('Product of type F',6),
('Product of type G',7),('Product of type H',8),('Product of type I',9),
('Product of type J',10),('Product of type K',11),('Product of type L',12),
('Product of type M',13),('Product of type N',14),('Product of type O',15),
('Product of type P',16),('Product of type Q',17),('Product of type R',18),
('Product of type S',19),('Product of type T',20),('Product of type U',21),
('Product of type V',22),('Product of type W',23),('Product of type X',24),
('Product of type Y',25),('Product of type Z',26);

update dbo.Entities set EntityDescription = '<descriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescriptiondescription>';
update dbo.Entities set EntityPrice = 100500;