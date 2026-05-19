using Domain.Entities.Team;
using Domain.Interfaces;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly AppDbContext _context;

    public TeamMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeamMember>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers
            .ToListAsync(cancellationToken);
    }

    public async Task<TeamMember?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task AddAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
    {
        await _context.TeamMembers.AddAsync(teamMember, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
    {
        _context.TeamMembers.Update(teamMember);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
    {
        _context.TeamMembers.Remove(teamMember);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
