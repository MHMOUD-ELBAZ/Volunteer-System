using Demo.Business.DTOs.Skill;
using Demo.Business.Mappers;



namespace Demo.Business.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IOpportunitySkillRepository _oSRepository;

        public SkillService(ISkillRepository skillRepository, IOpportunitySkillRepository oSRepository)
        {
            _skillRepository = skillRepository;
            _oSRepository = oSRepository;
        }

        public SkillDto? GetSkillById(int id)
        {
            var skill = _skillRepository.GetSkill(id);
            return skill != null ? SkillMapper.MapToSkillDto(skill) : null;
        }

        public SkillWithOpportunitiesDto? GetSkillWithOpportunities(int id)
        {
            var skill = _skillRepository.GetSkillWithOpportunities(id);
            
            if(skill == null) return null;

            skill.OpportunitySkills = new List<OpportunitySkill>(_oSRepository.GetOpportunitiesWithSkill(id)); 
            return SkillMapper.MapToSkillWithOpportunitiesDto(skill);
        }

        public SkillWithVolunteersDto? GetSkillWithVolunteers(int id)
        {
            var skill = _skillRepository.GetSkillWithVolunteers(id);
            return skill != null ? SkillMapper.MapToSkillWithVolunteersDto(skill) : null;
        }

        public IEnumerable<SkillDto> GetAllSkills()
        {
            var skills = _skillRepository.GetAll().ToList();
            return skills.Select(SkillMapper.MapToSkillDto);
        }

        public SkillDto CreateSkill(CreateSkillDto skillDto)
        {
            var skill = SkillMapper.MapToSkill(skillDto);
            _skillRepository.Add(skill);
            _skillRepository.Save();
            return SkillMapper.MapToSkillDto(skill);
        }

        public SkillDto? UpdateSkill(int id, SkillDto skillDto)
        {
            var skill = _skillRepository.GetSkill(id);
            if (skill == null) return null;

            skill.Name = skillDto.Name;
            skill.Descritption = skillDto.Descritption;

            _skillRepository.Update(skill);
            _skillRepository.Save();

            return SkillMapper.MapToSkillDto(skill);
        }

        public bool DeleteSkill(int id)
        {
            var skill = _skillRepository.GetSkill(id);
            if (skill == null) return false;

            _skillRepository.Delete(skill);
            _skillRepository.Save();

            return true;
        }
    }
}
