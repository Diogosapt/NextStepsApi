using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextSteps.Api.Dto;
using NextSteps.Business.Core.Common;
using NextSteps.Business.UsesCases;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NextSteps.Api.Controllers.v1
{
    using NextSteps.Business.Models;

    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public PersonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create Person
        /// </summary>
        [HttpPost]
        [Route("", Name = "Create")]
        [ProducesResponseType(typeof(ApiResult<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<Guid>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResult<Guid>), (int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> CreateAsync(PersonCreateDto person)
        {
            var entity = _mapper.Map<PersonCreateDto, Person>(person);

            var result = await _mediator.Send(new PersonCreateCommand(entity));

            if (result.OK)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        [HttpPut]
        [Route("", Name = "Update")]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> UpdateAsync(PersonDto person)
        {
            var entity = _mapper.Map<PersonDto, Person>(person);

            var result = await _mediator.Send(new PersonUpdateCommand(entity));

            if (result.OK)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        /// <summary>
        /// Delete Person
        /// </summary>
        [HttpDelete]
        [Route("{id}", Name = "Delete")]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new PersonDeleteCommand(id);

            var result = await _mediator.Send(command);

            if (result.OK)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        /// <summary>
        /// Get Person by Id
        /// </summary>
        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [ProducesResponseType(typeof(ApiResult<PersonDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<PersonDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApiResult<PersonDto>), (int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> PersonGetByIdAsync(Guid id)
        {
            var response = new ApiResult<PersonDto>();

            var result = await _mediator.Send(new PersonGetByIdQuery(id));

            if (result.OK)
            {
                response.Data = _mapper.Map<Person, PersonDto>(result.Data);
                response.CopyMessages(result);
                return Ok(response);
            }
            else
            {
                return NotFound(result);
            }
        }

        /// <summary>
        /// Get a paged list of Person according to the specified filter
        /// </summary>
        [HttpGet]
        [Route("Search/{page}/{pageSize}", Name = "SearchPersonPaged")]
        [ProducesResponseType(typeof(ApiResult<PagedResult<PersonDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<PagedResult<PersonDto>>), (int)HttpStatusCode.ServiceUnavailable)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> SearchPersonPagedAsync([FromQuery] FilterPersonDto filter, int page = 1, int pageSize = 5)
        {
            var response = new ApiResult<PagedResult<PersonDto>>();

            var result = await _mediator.Send(new PersonSearchQuery(_mapper.Map<FilterPersonDto, Filters>(filter), page, pageSize));

            response.Data = new PagedResult<PersonDto>
            {
                Page = result.Data.Page,
                PageSize = result.Data.PageSize,
                Total = result.Data.Total,
                Results = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(result.Data.Results)
            };

            response.CopyMessages(result);
            return Ok(response);
        }
    }
}