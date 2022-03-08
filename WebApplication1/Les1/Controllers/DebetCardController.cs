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

        public DebetCardController(IDebetCardRepository debetCardRepository, IMapper mapper)
        {
            _debetCardRepository = debetCardRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task Add(DebetCardResponse request)
        {
            await _debetCardRepository.Add(_mapper.Map<DebetCard>(request));
        }

        [HttpGet]
        public async Task<IEnumerable<DebetCardRequest>> Get()
        {
            var data = await _debetCardRepository.Get();
            return data.Select(_mapper.Map<DebetCardRequest>);
        }

        [HttpDelete("{id:int}")]
        public async Task DeleteAsync([FromRoute] int id)
        {
            await _debetCardRepository.Delete(id);
        }

        [HttpPut]
        public async Task UpdateAsync(DebetCardResponse request)
        {
            await _debetCardRepository.Update(_mapper.Map<DebetCard>(request));
        }
    }
}
