using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    //
    [ApiController]
    [Route("items")]
    public class ItemsController:ControllerBase
    {
        private readonly ItemsRepository itemsRepository =new();

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync(){
            var items = (await itemsRepository.GetAllAsync())
                        .Select(item=>item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id){

            var item = await itemsRepository.GetAsync(id);
            if(item == null)  
            return  NotFound("item not found");

            return item.AsDto();
        }
        
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto){
            
            var item = new Item{
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto){
            
            var existingItem = await itemsRepository.GetAsync(id);
            if(existingItem==null)
            return  NotFound("item not found");

              existingItem.Name = updateItemDto.Name;
              existingItem.Description = updateItemDto.Description;
              existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){

             var item = await itemsRepository.GetAsync(id);
            if(item == null)  
            return  NotFound("item not found");

            await itemsRepository.RemoveAsync(id);

            return NoContent();
        }
    }
    
}