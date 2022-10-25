using InnoGotchi.BusinessLogic.Dto;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IVitalSignService
    {
        public Task<int?> CreateVitalSignAsync(VitalSignDto vitalSign);
        public Task UpdateVitalSignAsync(VitalSignDto vitalSign);
        public Task<VitalSignDto?> GetVitalSignByIdAsync(int vitalSignId);
        public Task<PetDto?> GetPetByIdAsync(int petId);

    }
}
