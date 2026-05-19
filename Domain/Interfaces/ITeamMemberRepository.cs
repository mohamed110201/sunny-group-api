using Domain.Entities.Team;

namespace Domain.Interfaces;

public interface ITeamMemberRepository
{
    Task<List<TeamMember>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TeamMember?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
    Task UpdateAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
    Task DeleteAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
}
