create table AvitoApartmentDefinition 
(
    Id serial,
    City text character set utf8mb4 collate utf8mb4_unicode_ci null,
    AvitoId text character set utf8mb4 collate utf8mb4_unicode_ci null
);
create table CianApartmentDefinition 
(
    Id serial,
    CianId text character set utf8mb4 collate utf8mb4_unicode_ci null
);
create table ApartmentData
(
    Id serial,
    Name text character set utf8mb4 collate utf8mb4_unicode_ci null,
    Price bigint unsigned not null
);