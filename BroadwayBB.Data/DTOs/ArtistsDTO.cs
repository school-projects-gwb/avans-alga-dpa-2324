using BroadwayBB.Data.DTOs;
using BroadwayBB.Data.Strategies.Interfaces;

namespace BroadwayBB.Data.Strategies;

public struct ArtistsDTO : DTO
{
    public List<ArtistDTO> Artists;

    public ArtistsDTO()
    {
        Artists = new List<ArtistDTO>();
    }
}