using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IVitalSignService
    {
        Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd);
        Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate);
        Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId);
        Task<VitalSignDto?> GetVitalSignByPetIdAsync(int petId);

    }
}
