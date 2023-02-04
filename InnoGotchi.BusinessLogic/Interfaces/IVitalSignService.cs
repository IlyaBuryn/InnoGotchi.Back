using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IVitalSignService
    {
        public Task<int?> CreateVitalSignAsync(VitalSignDto vitalSignToAdd);
        public Task<bool> UpdateVitalSignAsync(VitalSignDto vitalSignToUpdate);
        public VitalSignDto? GetVitalSignById(int vitalSignId);
        public VitalSignDto? GetVitalSignByPetId(int petId);

    }
}
