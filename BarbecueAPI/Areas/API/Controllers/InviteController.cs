using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using BarbecueAPI.Controllers;
using BarbecueAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Db;
using Models.Db.Account;
using Models.DTOs.Invites;
using Models.DTOs.Misc;
using Services.ApiServices.Abstractions;

namespace BarbecueAPI.Areas.API.Controllers
{
    public class InviteController : BarbecueApiController
    {
        private readonly IInviteService _inviteService;

        public InviteController(ITokenSessionService tokenSessionService, IInviteService inviteService) : base(tokenSessionService)
        {
            _inviteService = inviteService;
        }

        [HttpPost]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateInviteDto createInviteDto)
        {
            try
            {
                var createdDto = await _inviteService.CreateInvite(createInviteDto);

                return Ok(createdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Accept([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.AcceptInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Reject([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.RejectInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult> Cancel([Id(typeof(Invite))] long id)
        {
            try
            {
                await _inviteService.CancelInvite(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetIssued([Id(typeof(User))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetIssued(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetReceived([Id(typeof(User))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetReceived(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<ICollection<InviteWithIdDto>>> GetByGroup([Id(typeof(Group))] long id)
        {
            try
            {
                var inviteWithIdDtos = await _inviteService.GetByGroup(id);

                return Ok(inviteWithIdDtos);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(AuthTokenFilter))]
        public async Task<ActionResult<InviteWithIdDto>> GetById([Id(typeof(Invite))] long id)
        {
            try
            {
                var inviteWithIdDto = await _inviteService.GetById(id);

                return Ok(inviteWithIdDto);
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        [HttpGet]
        public Task<string> GetQrUrl([Id(typeof(Invite))] long id)
        {
            string inviteUrl = $"http://akiana.io:8080/api/invite/getbyid?id={id}";
            int margin = 10;
            int size = 400;
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chld={2}|{3}&chs={0}x{0}&chl={1}",
                size, HttpUtility.UrlEncode(inviteUrl), QRCodeErrorCorrectionLevel.Medium.ToString()[0], margin
            );
            return Task.FromResult(url);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetQrHtml([Id(typeof(Invite))] long id)
        {
            try
            {
                int margin = 10;
                int size = 400;
                var url = await GetQrUrl(id);
                string html =
                    "<!DOCTYPE html>" +
                    "<html lang=\"en\">" +
                    "<head>" +
                    "<meta charset=\"UTF-8\">" +
                    "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">" +
                    "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                    "<title>Barbecue Finance</title>" +
                    "</head>" +
                    "<body style=\"background: black; margin:0;padding:0\">" +
                    $"<div style=\"position: absolute; top: 50%; transform: translateY(-50%); text-align:center;width:100%;\"><img src='{url}' width=\"{size}\" height=\"{size}\"></div>" +
                    "</body>" +
                    "</html>";
                return new ContentResult() {Content = html, StatusCode = 200, ContentType = "text/html"};
            }
            catch (Exception ex)
            {
                return BarbecueError(ex.Message);
            }
        }

        public enum QRCodeErrorCorrectionLevel
        {
            /// <summary>Recovers from up to 7% erroneous data.</summary>
            Low,

            /// <summary>Recovers from up to 15% erroneous data.</summary>
            Medium,

            /// <summary>Recovers from up to 25% erroneous data.</summary>
            QuiteGood,

            /// <summary>Recovers from up to 30% erroneous data.</summary>
            High
        }
    }
}