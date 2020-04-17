create database if not exists db_auftrag collate utf8_general_ci;
use db_auftrag;

create table requests(
	id int not null auto_increment,
    firstname varchar(100) null,
    lastname varchar(100) not null,
    email varchar(100) not null,
	telnum varchar(100) not null,
    dateArrival date not null,
    dateDeparture date not null,
    CountOfPeople int not null,
	comments varchar(1000),
    
	constraint id_PK primary key(id) 
      
)engine=InnoDB;

create table users(
	id int not null auto_increment,
    firstname varchar(100) not null,
    lastname varchar(100) not null,
    email varchar(100) not null,
    userRole int not null,
    password varchar(128) not null,
    
    constraint id_PK primary key(id)
)engine=InnoDB;

INSERT INTO requests VALUES(null,"Lukas","Heber","luheber@tsn.at","43664-3214322","2021-4-1","2021-4-5",2,"Testeintrag blablabla");
INSERT INTO users VALUES(null,"Lukas","Heber","luheber@tsn.at",1,sha2("passwort",256));

select * from requests;

select* from users;
