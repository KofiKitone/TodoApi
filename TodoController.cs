using System.Threading;
using System.ComponentModel;
using System.Transactions;
using System.Xml.XPath;
using System.Net.Http.Headers;
using System.Data;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TodoController(ApplicationDbContext context)
        {
            this.context = context;    
        }

        //GET
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll() 
        {   
            var todo = from t in context.Todo select t;

            todo = todo.OrderByDescending(t => t.Date);
            return Ok(todo);
        } 

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            if (id != null)
            {
                var item = await context.Todo.FindAsync(id);
                if (item != null)
                {
                    return Ok(item);
                }
            }
            return NotFound();
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest todo)
        {
            var item = new Todo()
            {
                id = new Guid(),
                Title = todo.Title,
                Activity = todo.Activity,
                Date = todo.Date,
                Time = todo.Time,
                Done = todo.Done
            };
            await context.Todo.AddAsync(item);
            await context.SaveChangesAsync();
            return Ok();
        }

        //PUT
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> EditTodo([FromRoute] Guid id, EditTodoRequest todo)
        {
            if (id != null)
            {
                var item = context.Todo.Find(id);
                if (item != null)
                {
                    item.Title = todo.Title;
                    item.Activity = todo.Activity;
                    item.Time = todo.Time;
                    item.Date = todo.Date;
                    item.Done = todo.Done;
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            return NotFound();
        }

        //DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            if (id != null)
            {
                var item = await context.Todo.FindAsync(id);
                if (item != null)
                {
                    context.Todo.Remove(item);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
                
            return NotFound();
            
        }


        //mark as done 
        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> markDone([FromRoute] Guid id)
        {
            if (id != null)
            {
                var item = await context.Todo.FindAsync(id);
                if (item != null)
                {
                    item.Title = item.Title;
                    item.Activity = item.Activity;
                    item.Time = item.Time;
                    item.Date = item.Date;
                    item.Done = false;
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            return NotFound();
        }

        


    }
}