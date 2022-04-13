using AutoMapper;
using DataLayer;
using Les1.DAL;
using Les1.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Les1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DebetCardController : ControllerBase
    {
        private readonly IDebetCardRepository _debetCardRepository;
        private readonly IMapper _mapper;
        private readonly IDebetCardCreateValidation _createRequestValidator;
        private readonly IDebetCardUpdateValidation _updateRequestValidator;

        public DebetCardController(IDebetCardRepository debetCardRepository, IMapper mapper, IDebetCardCreateValidation createRequestValidator , IDebetCardUpdateValidation updateRequestValidator)
        {
            _debetCardRepository = debetCardRepository;
            _mapper = mapper;
            _createRequestValidator = createRequestValidator;
            _updateRequestValidator = updateRequestValidator;
        }

        [HttpPost]
        public async Task<DebetCardCreateResponse> Add(DebetCardRequest request)
        {
            var failures = _createRequestValidator.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return new DebetCardCreateResponse(failures, false);
            }
            await _debetCardRepository.Add(_mapper.Map<DebetCard>(request));
            return new DebetCardCreateResponse(failures, true);
        }

        [HttpGet]
        public async Task<IEnumerable<DebetCardResponse>> Get()
        {
            var data = await _debetCardRepository.Get();
            return data.Select(_mapper.Map<DebetCardResponse>);
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteAsync([FromRoute] int id)
        {
            await _debetCardRepository.Delete(id);
        }

        [HttpPut]
        public async Task<DebetCardUpdateResponse> UpdateAsync(DebetCardRequest request)
        {
            var failures = _updateRequestValidator.ValidateEntity(request);
            if (failures.Count > 0)
            {
                return new DebetCardUpdateResponse(failures, false);
            }
            await _debetCardRepository.Update(_mapper.Map<DebetCard>(request));
            return new DebetCardUpdateResponse(failures, true);
        }
    }
}
