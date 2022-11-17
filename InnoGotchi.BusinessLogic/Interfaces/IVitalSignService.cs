using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IVitalSignService
    {
        public Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd);
        public Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate);
        public Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId);
        public Task<VitalSignDto?> GetVitalSignByPetIdAsync(int petId);

    }
}
