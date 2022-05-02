# Adapters

## Persistence

### EFCore MySql Database

#### Create migration

```bash
dotnet ef migrations add MyMigration --project RssManager.Adapters --output-dir Persistence/MySqlEfCorePersistence/Migrations
```

#### Migrate database

```bash
dotnet ef database update --project RssManager.Adapters
```

## Providers