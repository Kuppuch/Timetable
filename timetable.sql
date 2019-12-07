-- phpMyAdmin SQL Dump
-- version 5.0.0-rc1
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Дек 07 2019 г., 15:37
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
(19, 'Методы и программные средства вычислений', 13);

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
(4, 'ИСБ-1', 17),
(5, 'ВТ-1', 17),
(6, 'ПРИ-1', 18),
(7, 'ИСТ-1', 18),
(8, 'ИБ-1', 18),
(9, 'ИСБ-1', 18),
(10, 'ВТ-1', 18),
(11, 'ПРИ-1', 19),
(12, 'ИСТ-1', 19),
(13, 'ИБ-1', 19),
(14, 'ИСБ-1', 19),
(15, 'ВТ-1', 19);

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
(19, 19, 7, 13);

-- --------------------------------------------------------

--
-- Структура таблицы `type_user`
--

CREATE TABLE `type_user` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `type_user`
--

INSERT INTO `type_user` (`id`, `name`) VALUES
(1, 'Студент'),
(2, 'Преподаватель'),
(3, 'Работник');

-- --------------------------------------------------------

--
-- Структура таблицы `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `group` int(11) DEFAULT NULL,
  `type` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `user`
--

INSERT INTO `user` (`id`, `name`, `group`, `type`) VALUES
(1, 'Куппе Р.О', 1, 1),
(2, 'Савин М.К.', 1, 1),
(3, 'Дмитриев М.А.', 1, 1),
(4, 'Евдокимов С.П.', 2, 1),
(5, 'Жаравина А.С.', 2, 1),
(6, 'Шумейко Д.С.', 2, 1),
(7, 'Кузин Д.В.', 3, 1),
(8, 'Бурмистров Д.А.', 4, 1),
(9, 'Волченков Д.Д.', 5, 1),
(10, 'Вершинин В.В.', NULL, 2),
(11, 'Жигалов И.Е.', NULL, 2),
(12, 'Озерова М.И', NULL, 2),
(13, 'Кириллова С.Ю.', NULL, 2),
(14, 'Тимофеев А.А.', NULL, 2),
(15, 'Шамышева О.Н.', NULL, 2),
(16, 'Монахова Г.Е.', NULL, 2),
(17, 'Бородина Е.К.', NULL, 2),
(18, 'Проскурина Г.В.', NULL, 2),
(19, 'Койкова Т.В.', NULL, 2),
(20, 'Тарасевич О.Д.', NULL, 2),
(21, 'Соловьёва В.В.', NULL, 2),
(22, 'Дубровин Н.И.', NULL, 2),
(23, 'Макаров Р.И.', NULL, 2);

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
-- Индексы таблицы `type_user`
--
ALTER TABLE `type_user`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD KEY `group_idx` (`group`),
  ADD KEY `type_idx` (`type`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `discipline`
--
ALTER TABLE `discipline`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT для таблицы `group`
--
ALTER TABLE `group`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT для таблицы `lesson`
--
ALTER TABLE `lesson`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT для таблицы `type_user`
--
ALTER TABLE `type_user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT для таблицы `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `discipline`
--
ALTER TABLE `discipline`
  ADD CONSTRAINT `lecturer` FOREIGN KEY (`user`) REFERENCES `user` (`id`);

--
-- Ограничения внешнего ключа таблицы `lesson`
--
ALTER TABLE `lesson`
  ADD CONSTRAINT `academic_group` FOREIGN KEY (`group`) REFERENCES `group` (`id`),
  ADD CONSTRAINT `discipline` FOREIGN KEY (`discipline`) REFERENCES `discipline` (`id`),
  ADD CONSTRAINT `teacher` FOREIGN KEY (`teacher`) REFERENCES `user` (`id`);

--
-- Ограничения внешнего ключа таблицы `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `group` FOREIGN KEY (`group`) REFERENCES `group` (`id`),
  ADD CONSTRAINT `type` FOREIGN KEY (`type`) REFERENCES `type_user` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

