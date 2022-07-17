# cSharpClientApp


Техническое задание:
Необходимо разработать приложение с графическим интерфейсом с использованием технологии WPF на языке C#. Приложение должно работать в операционной системе Windows. Необходимо использовать платформу Net Framework 4.7.2

Приложение имеет следующую функциональность.

В окне приложения отображаются 2 вкладки:
1 вкладка: "Клиенты"
2 вкладка: "Заявки"

На первой вкладке имеются:
1. Нижняя часть (150 пикс. по высоте)
    Содержит блок с текстом "Примечание".
2. Верхняя часть (занимает все остальное пространство по вертикали) содержит 2 таблицы:
     2.1. Левая таблица, список клиентов (2/3 ширины окна)
        Колонки таблицы:
          -  наименование
          -  инн
          -  сфера деятельности
          -  количество заявок
          -  дата последней заявки
     2.2. Правая таблица, список заявок (1/3 ширины окна)
          -  дата заявки
          -  наименование работ
          -  описание работ
          -  статус (новая, в работе, выполнена)

В списке клиентов отображаются все клиенты отсортированные в алфавитном порядке.
При выборе одного из клиентов (мышью или с помощью клавиатуры) в списке заявок отображаются заявки, принадлежащие выбранному клиенту, а в поле "Примечание" - текст примечания для данного клиента.

Должен быть реализован следующий функционал:
- добавление, изменение и удаление клиентов
- добавление и удаление заявки
Операции по работе с клиентами и заявками должны происходить в отдельных всплывающих окнах.

На второй вкладке должна отображаться таблица со всеми заявками по всем клиентам, отсортированных по убыванию даты создания.

Для таблицы добавить фильтр по клиенту в виде выпадающего списка. По умолчанию в этом списке ничего не выбрано. Если выбран конкретный клиент, то отображаются только заявки, принадлежащие выбранному клиенту.

Должен быть реализован функционал изменения статуса заявки.

Все таблицы с данными должны иметь возможность сортировки строк по выбранной колонке (кликом мыши на заголовке столбца).

Данные приложения должны храниться в базе данных (MySQL/PostgreSQL или любой другой реляционной БД). Необходимо разработать структуру таблиц в базе данных и заполнить их тестовыми записями.
