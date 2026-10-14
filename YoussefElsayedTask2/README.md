# Task - Advanced EF Core (Product & Category)

ASP.NET Core Web API (.NET 10) + EF Core 10 + SQL Server.

## Mapping المتطلبات للكود

| # | المطلوب | مكانه | الفكرة |
|---|---------|-------|--------|
| 1 | GET + AsNoTracking + Select + Pagination | `ProductsController.GetAll` | قراءة فقط بدون Tracking، أعمدة محددة، `Skip/Take` مع ترتيب ثابت |
| 2 | Category Name بدون N+1 | `ProductsController.GetAll` | الاسم داخل الـ `Select` فـ EF يعمل JOIN واحد بدل Query لكل منتج |
| 3 | Index مناسب | `AppDbContext` → `HasIndex(p => p.Name)` | `Name` هو الحقل الأكثر بحثًا → Index Seek بدل Table Scan (يفيد مع `StartsWith` مش `Contains`) |
| 4 | Soft Delete | `Product.IsDeleted` + `HasQueryFilter` + `DELETE` | الصف بيتعلّم محذوف والـ Filter بيخفيه من كل الـ Queries تلقائيًا |
| 5 | PUT للحقول المسموحة + Change Tracking | `UpdateProductDto` + `ProductsController.Update` | نحمّل الـ Entity بـ Tracking، نعدّل Name/Description/Price فقط، فـ EF يولّد UPDATE للأعمدة المتغيرة بس |
| 6 | Concurrency Token + DbUpdateConcurrencyException | `Product.RowVersion` + `Update` | `rowversion` + ضبط `OriginalValue` من الـ Client + رجوع `409 Conflict` عند التعارض |

## التشغيل

```bash
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add Init
dotnet ef database update
dotnet run
```

عدّل الـ Connection String في `appsettings.json` حسب السيرفر عندك.

## Endpoints

- `GET    /api/products?page=1&pageSize=10&search=abc`
- `PUT    /api/products/{id}`  (Body: `name`, `description`, `price`, `rowVersion` من آخر GET)
- `DELETE /api/products/{id}`  (Soft Delete)
