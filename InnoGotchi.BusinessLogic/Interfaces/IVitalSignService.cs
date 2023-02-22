using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IVitalSignService
    {
        Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd);
        Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate);
        VitalSignDto? GetVitalSignById(int vitalSignId);
        VitalSignDto? GetVitalSignByPetId(int petId);

    }
}
