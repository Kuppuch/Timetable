-- phpMyAdmin SQL Dump
-- version 5.0.0-rc1
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Дек 16 2019 г., 21:54
-- Версия сервера: 8.0.18
-- Версия PHP: 7.4.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `timetable`
--

-- --------------------------------------------------------

--
-- Структура таблицы `discipline`
--

CREATE TABLE `discipline` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `discipline`
--

INSERT INTO `discipline` (`id`, `name`, `user`) VALUES
(1, 'Технологии программирования', 10),
(2, 'Управление данными', 10),
(3, 'Математическое моделирование графических объектов', 11),
(4, 'Основы информационного дизайна', 12),
(5, 'Мультимедиа технологии', 12),
(6, 'Графические информационные технологии', 12),
(7, 'Распределённые программные системы', 14),
(8, 'Введение в профессию', 13),
(9, 'Основы алгоритмизации и программирования', 13),
(10, 'Алгоритмы и структуры данных', 13),
(11, 'Теоретические основы дискретных вычислений', 15),
(12, 'Интерактивные графические системы', 16),
(13, 'Платформонезависимое программирование', 18),
(14, 'Иностранный язык', 19),
(15, 'Физическая культура и спорт', 20),
(16, 'История', 21),
(17, 'Математика', 22),
(18, 'Методы анализа данных', 23),
(19, 'Методы и программные средства вычислений', 13),
(23, 'х', 17);

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `discipline_view`
-- (См. Ниже фактическое представление)
--
CREATE TABLE `discipline_view` (
`id` int(11)
,`name` varchar(100)
,`user_id` int(11)
,`user` varchar(45)
);

-- --------------------------------------------------------

--
-- Структура таблицы `group`
--

CREATE TABLE `group` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `year` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `group`
--

INSERT INTO `group` (`id`, `name`, `year`) VALUES
(1, 'ПРИ-1', 17),
(2, 'ИСТ-1', 17),
(3, 'ИБ-1', 17),
(4, 'ИСБ-1', 18),
(5, 'ВТ-1', 17),
(6, 'ПРИ-1', 18),
(7, 'ИСТ-1', 18),
(8, 'ИБ-1', 18),
(9, 'ИСБ-1', 17),
(10, 'ВТ-1', 18),
(11, 'ПРИ-1', 19),
(12, 'ИСТ-1', 19),
(13, 'ИБ-1', 19),
(14, 'ИСБ-1', 19),
(15, 'ВТ-1', 19);

--
-- Триггеры `group`
--
DELIMITER $$
CREATE TRIGGER `group_AFTER_DELETE` AFTER DELETE ON `group` FOR EACH ROW BEGIN
	DELETE FROM `lesson` where `group` = OLD.`id`;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `group_BEFORE_DELETE` BEFORE DELETE ON `group` FOR EACH ROW BEGIN
	DELETE FROM `lesson` where `group` = OLD.id;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `less`
-- (См. Ниже фактическое представление)
--
CREATE TABLE `less` (
`id` int(11)
,`discipline_id` int(11)
,`group_id` int(11)
,`teacher_id` int(11)
,`discipline` varchar(100)
,`group` varchar(45)
,`year` int(11)
,`teacher` varchar(45)
);

-- --------------------------------------------------------

--
-- Структура таблицы `lesson`
--

CREATE TABLE `lesson` (
  `id` int(11) NOT NULL,
  `discipline` int(11) NOT NULL,
  `group` int(11) NOT NULL,
  `teacher` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `lesson`
--

INSERT INTO `lesson` (`id`, `discipline`, `group`, `teacher`) VALUES
(1, 1, 1, 10),
(2, 2, 6, 10),
(3, 3, 1, 11),
(4, 4, 2, 12),
(5, 5, 7, 12),
(6, 6, 11, 12),
(7, 7, 1, 14),
(8, 8, 12, 13),
(9, 9, 13, 17),
(10, 10, 14, 13),
(11, 11, 15, 15),
(12, 12, 2, 16),
(13, 13, 1, 18),
(14, 14, 11, 19),
(15, 15, 5, 20),
(16, 16, 11, 21),
(17, 17, 12, 22),
(18, 18, 7, 23),
(19, 19, 7, 13),
(20, 15, 15, 23);

-- --------------------------------------------------------

--
-- Структура таблицы `timetable`
--

CREATE TABLE `timetable` (
  `id` int(11) NOT NULL,
  `lesson` int(11) NOT NULL,
  `weekday` int(11) NOT NULL,
  `numerator` tinyint(4) NOT NULL,
  `number` int(11) NOT NULL,
  `location` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `timetable`
--

INSERT INTO `timetable` (`id`, `lesson`, `weekday`, `numerator`, `number`, `location`) VALUES
(1, 1, 1, 1, 1, '410-2'),
(2, 2, 2, 1, 1, '410-2'),
(3, 3, 3, 1, 2, '213-3'),
(4, 4, 4, 1, 3, '213-3'),
(5, 5, 5, 1, 2, '213-3'),
(6, 6, 1, 0, 1, '213-3'),
(7, 7, 2, 0, 2, '314-3'),
(8, 8, 3, 0, 2, '410-2'),
(9, 9, 4, 0, 3, '410-2'),
(10, 10, 5, 0, 2, '414-2'),
(11, 11, 1, 1, 2, '418-2'),
(12, 12, 2, 1, 2, '314-3'),
(13, 13, 3, 1, 1, '410-2'),
(14, 14, 4, 1, 2, '403-1'),
(15, 15, 5, 1, 1, '1'),
(16, 16, 1, 0, 2, 'А-3'),
(17, 17, 2, 0, 1, 'Б-3'),
(18, 18, 3, 0, 1, '404а-2'),
(19, 19, 4, 0, 2, '414-2'),
(20, 20, 5, 1, 6, '101'),
(21, 17, 4, 1, 6, 'Б-3'),
(22, 16, 4, 1, 6, 'Ф-3'),
(23, 17, 2, 0, 5, 'Ъ-3');

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `timetable_view`
-- (См. Ниже фактическое представление)
--
CREATE TABLE `timetable_view` (
`id` int(11)
,`id_lesson` int(11)
,`discipline` varchar(100)
,`id_user` int(11)
,`teacher` varchar(45)
,`weekday` int(11)
,`numerator` tinyint(4)
,`number` int(11)
,`location` varchar(45)
,`id_group` int(11)
,`group_name` varchar(45)
,`group_year` int(11)
);

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `group` int(11) DEFAULT NULL,
  `type` int(11) NOT NULL,
  `email` varchar(75) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `name`, `group`, `type`, `email`) VALUES
(1, 'Савин М.К', 1, 3, 'mksavin@mail.ru'),
(2, 'Куппе Р.О', 1, 3, 'kuppa@ro.man'),
(3, 'Алексеев Р.И', NULL, 2, 'alexseew@ivan.ivanov.ich'),
(4, 'Антонов А.Х', NULL, 1, 'anton_ov@fiz.ha'),
(5, 'Жаравина А.С', 2, 3, 'Zhar@vi.na'),
(6, 'Шумейко Д.С', 2, 3, 'Shum@eq.ko'),
(7, 'Кузин Д.В', 3, 3, 'kyzma@d.w'),
(8, 'Бурмистров Д.А', 4, 3, 'Bur@mirstr.off'),
(9, 'Волченков Д.Д', 5, 3, 'Wolf@.Chenk.off'),
(10, 'Вершинин В.В', NULL, 2, 'Vershinin@VV.VV'),
(11, 'Жигалов И.Е', NULL, 2, 'Zhigalov@.yandox.con'),
(12, 'Озерова М.И', NULL, 2, 'Ozerova@MI.ru'),
(13, 'Кириллова С.Ю', NULL, 2, 'Kirillora@C.U'),
(14, 'Тимофеев А.А', NULL, 2, 'Wirbel@java.ru'),
(15, 'Шамышева О.Н', NULL, 2, 'Shamu@she.va'),
(16, 'Монахова Г.Е', NULL, 2, 'Monahova@GE.ru'),
(17, 'Бородина Е.К', NULL, 2, 'Borodina@EK.ru'),
(18, 'Проскурина Г.В', NULL, 2, 'Proskurina@java.ru'),
(19, 'Койкова Т.В', NULL, 2, 'Kojkova@yaho.eng'),
(20, 'Тарасевич О.Д', NULL, 2, 'Fizra@4ev.er'),
(21, 'Соловьёва В.В', NULL, 2, 'Histori@ya.ru'),
(22, 'Дубровин Н.И', NULL, 2, 'DobroWIN@mathwars.su'),
(23, 'Макаров Р.И', NULL, 2, 'Makarov@RI.ch'),
(24, 'Дмитриев М.А', NULL, 3, 'dmitriev@miha.ill');

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `users_view`
-- (См. Ниже фактическое представление)
--
CREATE TABLE `users_view` (
`id` int(11)
,`name` varchar(45)
,`group` varchar(45)
,`year` int(11)
,`group_id` int(11)
,`email` varchar(75)
,`type_id` int(11)
,`type` varchar(45)
);

-- --------------------------------------------------------

--
-- Структура таблицы `user_type`
--

CREATE TABLE `user_type` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `user_type`
--

INSERT INTO `user_type` (`id`, `name`) VALUES
(1, 'Спец. по кадрам'),
(2, 'Преподаватель'),
(3, 'Студент');

-- --------------------------------------------------------

--
-- Структура для представления `discipline_view`
--
DROP TABLE IF EXISTS `discipline_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `discipline_view`  AS  select `discipline`.`id` AS `id`,`discipline`.`name` AS `name`,`users`.`id` AS `user_id`,`users`.`name` AS `user` from (`discipline` join `users`) where (`discipline`.`user` = `users`.`id`) ;

-- --------------------------------------------------------

--
-- Структура для представления `less`
--
DROP TABLE IF EXISTS `less`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `less`  AS  select `lesson`.`id` AS `id`,`discipline`.`id` AS `discipline_id`,`group`.`id` AS `group_id`,`users`.`id` AS `teacher_id`,`discipline`.`name` AS `discipline`,`group`.`name` AS `group`,`group`.`year` AS `year`,`users`.`name` AS `teacher` from (((`discipline` join `lesson`) join `users`) join `group`) where ((`lesson`.`group` = `group`.`id`) and (`lesson`.`discipline` = `discipline`.`id`) and (`lesson`.`teacher` = `users`.`id`)) ;

-- --------------------------------------------------------

--
-- Структура для представления `timetable_view`
--
DROP TABLE IF EXISTS `timetable_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `timetable_view` AS  select `timetable`.`id` AS `id`,`lesson`.`id` AS `id_lesson`,`discipline`.`name` AS `discipline`,`users`.`id` AS `id_user`,`users`.`name` AS `teacher`,`weekday` AS `weekday`,`numerator` AS `numerator`,`number` AS `number`,`location` AS `location`,`group`.`id` AS `id_group`,`group`.`name` AS `group_name`,`group`.`year` AS `group_year` from ((((`timetable` join `lesson`) join `discipline`) join `users`) join `group` on((`lesson`.`group` = `group`.`id`))) where ((`lesson` = `lesson`.`id`) and (`lesson`.`discipline` = `discipline`.`id`) and (`discipline`.`user` = `users`.`id`)) ;

-- --------------------------------------------------------

--
-- Структура для представления `users_view`
--
DROP TABLE IF EXISTS `users_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `users_view`  AS  select `users`.`id` AS `id`,`users`.`name` AS `name`,`group`.`name` AS `group`,`group`.`year` AS `year`,`group`.`id` AS `group_id`,`users`.`email` AS `email`,`user_type`.`id` AS `type_id`,`user_type`.`name` AS `type` from ((`users` left join `group` on((`users`.`group` = `group`.`id`))) join `user_type`) where (`users`.`type` = `user_type`.`id`) ;

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `discipline`
--
ALTER TABLE `discipline`
  ADD PRIMARY KEY (`id`),
  ADD KEY `lecturer_idx` (`user`);

--
-- Индексы таблицы `group`
--
ALTER TABLE `group`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `lesson`
--
ALTER TABLE `lesson`
  ADD PRIMARY KEY (`id`),
  ADD KEY `discipline_idx` (`discipline`),
  ADD KEY `group_idx` (`group`),
  ADD KEY `teacher_idx` (`teacher`);

--
-- Индексы таблицы `timetable`
--
ALTER TABLE `timetable`
  ADD PRIMARY KEY (`id`),
  ADD KEY `lesson_idx` (`lesson`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `group_idx` (`group`),
  ADD KEY `type_idx` (`type`);

--
-- Индексы таблицы `user_type`
--
ALTER TABLE `user_type`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `discipline`
--
ALTER TABLE `discipline`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT для таблицы `group`
--
ALTER TABLE `group`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT для таблицы `lesson`
--
ALTER TABLE `lesson`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT для таблицы `timetable`
--
ALTER TABLE `timetable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT для таблицы `user_type`
--
ALTER TABLE `user_type`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `discipline`
--
ALTER TABLE `discipline`
  ADD CONSTRAINT `lecturer` FOREIGN KEY (`user`) REFERENCES `users` (`id`);

--
-- Ограничения внешнего ключа таблицы `lesson`
--
ALTER TABLE `lesson`
  ADD CONSTRAINT `academic_group` FOREIGN KEY (`group`) REFERENCES `group` (`id`),
  ADD CONSTRAINT `discipline` FOREIGN KEY (`discipline`) REFERENCES `discipline` (`id`),
  ADD CONSTRAINT `teacher` FOREIGN KEY (`teacher`) REFERENCES `users` (`id`);

--
-- Ограничения внешнего ключа таблицы `timetable`
--
ALTER TABLE `timetable`
  ADD CONSTRAINT `lesson` FOREIGN KEY (`lesson`) REFERENCES `lesson` (`id`);

--
-- Ограничения внешнего ключа таблицы `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `group` FOREIGN KEY (`group`) REFERENCES `group` (`id`),
  ADD CONSTRAINT `type` FOREIGN KEY (`type`) REFERENCES `user_type` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

