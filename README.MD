create sequence id_karyawan start 1;
create sequence id_dep start 1;
CREATE TABLE public.karyawan
(
    id_karyawan varchar default 'ST'|| nextval('id_karyawan') primary key,
    nama character varying NOT NULL,
    id_dep varchar NOT NULL,
    PRIMARY KEY (id_karyawan)
);

ALTER TABLE IF EXISTS public.karyawan
    OWNER to postgres;

CREATE TABLE public.departemen
(
    id_dep varchar default 'ST'|| nextval('id_dep') primary key,
    nama_dep character varying(30) NOT NULL,
    PRIMARY KEY (id_dep)
);

ALTER TABLE IF EXISTS public.departemen
    OWNER to postgres;

--------------
INSERT FUNCTION
create or replace function st_insert
(
	_nama varchar,
	_id_dep varchar
)
RETURNS int AS
'
BEGIN
	insert into karyawan
	(
	nama,
	id_dep
	)
	values 
	(
	_nama,
	_id_dep
	);
	if found then 
		return 1;
	else 
		return 0;
	end if;
end
'

language plpgsql

-------------

SELECT FUNCTION
create function st_select()
returns table
(
	_id_karyawan varchar,
	_nama varchar,
	_id_dep varchar
)
	LANGUAGE plpgsql
	AS
'
begin 
	return query
	select id_karyawan, nama, id_dep from karyawan;
	end
'
----------------------
REPLACE FUNCTION
create or replace FUNCTION st_update
(
   	_id_karyawan varchar ,
	_nama varchar,
	_id_dep varchar
)
	returns int
	as
'
begin
	update karyawan set
		nama = _nama,
		id_dep = _id_dep
	WHERE id_karyawan=_id_karyawan;
	if found then
		return 1;
	else
		return 0;
	end if;
end
'

language plpgsql

-----------
DELETE FUNCTION
Create or replace function st_delete(_id_karyawan character varying)
Returns int AS
'
Begin
	Delete from karyawan
WHERE id_karyawan=_id_karyawan;
If found then
		Return 1;
Else 
		Return 0;
End if;
End
'
Language plpgsql; 
