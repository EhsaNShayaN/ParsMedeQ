namespace SRH.Persistance;

public struct DapperCommandDefinitionBuilder
{
    private string _commandText = string.Empty;
    private CommandType _commandType = CommandType.StoredProcedure;
    private Dictionary<string, object?>? _parameters = null;
    private CancellationToken? _cancellationToken = default;
    private CommandFlags _commandFlags = CommandFlags.None;
    private IDbTransaction? _dbTransaction = null;

    public DapperCommandDefinitionBuilder() { }

    public DapperCommandDefinitionBuilder SetProcedureName(string procedureName)
    {
        _commandText = procedureName;
        _commandType = CommandType.StoredProcedure;

        return this;
    }
    public DapperCommandDefinitionBuilder SetQueryText(string query)
    {
        _commandText = query;
        _commandType = CommandType.Text;

        return this;
    }
    public DapperCommandDefinitionBuilder SetParameter<T>(string name, T? value)
    {
        _parameters ??= new Dictionary<string, object?>();
        _parameters.Add(name, value);

        return this;
    }
    public DapperCommandDefinitionBuilder SetParameterIfNotNull<T>(string name, T? value)
    {
        if (value is null) return this;

        _parameters ??= new Dictionary<string, object?>();
        _parameters.Add(name, value);

        return this;
    }
    public DapperCommandDefinitionBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;

        return this;
    }
    public DapperCommandDefinitionBuilder SetCommandFlags(CommandFlags commandFlags)
    {
        _commandFlags = commandFlags;

        return this;
    }
    public DapperCommandDefinitionBuilder SetBuffered()
    {
        _commandFlags = CommandFlags.Buffered;
        return this;
    }
    public DapperCommandDefinitionBuilder SetPipelined()
    {
        _commandFlags = CommandFlags.Pipelined;
        return this;
    }
    public DapperCommandDefinitionBuilder SetNoCache()
    {
        _commandFlags = CommandFlags.NoCache;
        return this;
    }
    public DapperCommandDefinitionBuilder SetTransaction(IDbTransaction dbTransaction)
    {
        _dbTransaction = dbTransaction;
        return this;
    }

    public static DapperCommandDefinitionBuilder Procedure(string procedureName) =>
        new DapperCommandDefinitionBuilder().SetProcedureName(procedureName);
    public static DapperCommandDefinitionBuilder StreamedProcedure(string procedureName) =>
        new DapperCommandDefinitionBuilder().SetProcedureName(procedureName).SetPipelined();
    public static DapperCommandDefinitionBuilder Query(string query) =>
        new DapperCommandDefinitionBuilder().SetQueryText(query);

    public readonly CommandDefinition Build()
    {
        DynamicParameters? parameters = null;

        if (_parameters?.Any() ?? false)
        {
            parameters = new DynamicParameters();

            foreach (var (k, v) in _parameters)

            {
                parameters.Add(k, v);
            }
        }

        return new CommandDefinition(
                commandText: _commandText,
                parameters: parameters,
                commandType: _commandType,
                flags: _commandFlags,
                transaction: _dbTransaction,
                cancellationToken: _cancellationToken ?? CancellationToken.None);
    }
    public CommandDefinition Build(CancellationToken cancellationToken) =>
        this.WithCancellationToken(cancellationToken).Build();

}
