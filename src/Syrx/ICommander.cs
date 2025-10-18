//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx
{
    /// <summary>
    /// Provides comprehensive data access operations for repository pattern implementations.
    /// This interface serves as the primary abstraction for all database operations in Syrx,
    /// offering both synchronous and asynchronous methods for querying and executing commands.
    /// </summary>
    /// <typeparam name="TRepository">
    /// The repository type used for command resolution and type safety.
    /// This parameter enables automatic method-to-SQL mapping and provides scoped configuration
    /// based on the repository's namespace, type, and method names.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// The ICommander interface automatically resolves method names to SQL commands using the
    /// CallerMemberName attribute and hierarchical configuration lookup. This eliminates the need
    /// for manual command string management while maintaining full control over SQL execution.
    /// </para>
    /// <para>
    /// All database operations are fundamentally categorized as either:
    /// - Query operations: For data retrieval (SELECT statements)
    /// - Execute operations: For data modification (INSERT, UPDATE, DELETE statements)
    /// </para>
    /// <para>
    /// The interface supports complex scenarios including multi-mapping queries for object composition,
    /// multiple result sets, and transaction management through various overloads and extension points.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// public class UserRepository
    /// {
    ///     private readonly ICommander&lt;UserRepository&gt; _commander;
    ///     
    ///     public UserRepository(ICommander&lt;UserRepository&gt; commander)
    ///     {
    ///         _commander = commander;
    ///     }
    ///     
    ///     public async Task&lt;IEnumerable&lt;User&gt;&gt; GetAllUsersAsync()
    ///     {
    ///         return await _commander.QueryAsync&lt;User&gt;();
    ///     }
    ///     
    ///     public async Task&lt;User&gt; CreateUserAsync(User user)
    ///     {
    ///         var success = await _commander.ExecuteAsync(user);
    ///         return success ? user : null;
    ///     }
    /// }
    /// </code>
    /// </example>
    public partial interface ICommander<TRepository> : IDisposable
    {
    }
}