DELIMITER $$
CREATE DEFINER=`root`@`localhost` FUNCTION `VariableExch`(a date, b varchar(4)) RETURNS varchar(60) CHARSET utf8mb4
BEGIN
DECLARE res varchar(60);
set res = '';
if exists(select * from Exchange_Rates where Date=a and CharCode=b) then set res = (select val from Exchange_Rates where Date=a and CharCode=b );
else set res='К сожалению, курса валют на данную дату не обнаружено';
end if;
return res;
END$$
DELIMITER ;
