﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workshop_RSWEB.Data;
using Workshop_RSWEB.Models;

namespace Workshop_RSWEB.Pages.Predmets
{
    public class EditModel : PageModel
    {
        private readonly Workshop_RSWEB.Data.Workshop_RSWEBContext _context;

        public EditModel(Workshop_RSWEB.Data.Workshop_RSWEBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Predmet Predmet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Predmet = await _context.Predmet.FirstOrDefaultAsync(m => m.Id == id);

            if (Predmet == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Predmet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PredmetExists(Predmet.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PredmetExists(int id)
        {
            return _context.Predmet.Any(e => e.Id == id);
        }
    }
}
