commentsApiAspNet
### CRUD для сервиса комментариев
В проекте используется авторизация по токену, который хранится в БД.Для авторизации нужно указать заголовок **Authorization**, значения ключа хранится в БД, типы запросов, которые могут использоваться с этим ключем указаны в поле grantedRequest   
### Модель токена
```json
{
    "id": "5e237a37411af70f08ccf5aa",
    "key": "zzz",
    "grantedRequest": "POST,GET,PUT,DELETE",
    "isActive": true
}
```
* **id** - автогенерируемый ключ mongoDB
* **key** - токен для доступа к CRUD
* **grantedRequest** - список запросов, к которым разрешен доступ с этим ключем
* **isActive** - состояние ключа


Для удобства тестирования в проекте есть CRUD для работы с этими ключами. Использовать только для тестирования!!!:  

**GET odata/Tokens/** - получить список всех токенов  
**GET odata/Tokens({tokenValue})** - получить объект с указанным токеном. Так как значение ключа стринговое, следовательно задавать нужно в кавычках, например, Tokens('5e2abb71b0047908f8391bc2') 
**POST odata/Tokens** - Создать новый токен. В теле передать указанную выше модель.  
**PUT odata/Tokens({tokenValue})** - обновить объект с указанным токеном  
**DELETE odata/Tokens({tokenValue})** - удалить объект с указанным токеном  
Так же есть методы patch,merge

### Работа с комментариями  
Модель комментария:  

```json
{
    "id": "5e237b12effad2531ce36b35",
    "text": "hello",
    "dateCreate": "2020-01-18T21:39:30.165Z",
    "updatedOn": "2020-01-18T21:39:30.164Z",
    "createdOn": "2020-01-18T21:39:30.165Z",
    "author": "Alex",
    "topic": "TestComment"
}
```

**GET odata/Comments** - получить список всех комментариев   
**POST odata/Comments** - Создать новый комментарий. В ответ придет модель комментария с заполненным ID  
**PUT odata/Comments({id})** - обновить комментарий. Пример odata/Comments(э5e237b12effad2531ce36b35')  
**DELETE odata/Comments({id})** - удалить комментарий    

### Экспорт комментариев
Так как odata не предназначена для возврата файлов, для экспорта реализован отдельный контроллер 
**GET api/csv/export** - выгрузить список всех комментариев в csv файл. Название файла генерится в формате comments-{DateTime.Now}  
