# Wub-wub-wub-wub-wub-wub-wub-wub-wub

## Commands

### `dotent ef --help`
```powershell
Entity Framework Core .NET Command-line Tools 9.0.5

Usage: dotnet ef [options] [command]

Options:
  --version        Show version information
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  database    Commands to manage the database.
  dbcontext   Commands to manage DbContext types.
  migrations  Commands to manage migrations.

Use "dotnet ef [command] --help" for more information about a command.
```

### `dotnet ef migrations`
```powershell
Usage: dotnet ef migrations [options] [command]

Options:
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  add                        Adds a new migration.
  bundle                     Creates an executable to update the database.
  has-pending-model-changes  Checks if any changes have been made to the model since the last migration.
  list                       Lists available migrations.
  remove                     Removes the last migration.
  script                     Generates a SQL script from migrations.

Use "migrations [command] --help" for more information about a command.
```

### `dotnet ef dbcontext`
```powershell
Usage: dotnet ef dbcontext [options] [command]

Options:
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  info      Gets information about a DbContext type.
  list      Lists available DbContext types.
  optimize  Generates a compiled version of the model used by the DbContext.
  scaffold  Scaffolds a DbContext and entity types for a database.
  script    Generates a SQL script from the DbContext. Bypasses any migrations.

Use "dbcontext [command] --help" for more information about a command.
```

### `dotnet ef database`
```powershell
Usage: dotnet ef database [options] [command]

Options:
  -h|--help        Show help information
  -v|--verbose     Show verbose output.
  --no-color       Don't colorize output.
  --prefix-output  Prefix output with level.

Commands:
  drop    Drops the database.
  update  Updates the database to a specified migration.

Use "database [command] --help" for more information about a command.
```

