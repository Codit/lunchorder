import { Pipe, PipeTransform } from '@angular/core';
import { MenuEntry } from '../domain/dto/menuEntry';

@Pipe({name: 'menuEntryByCategoryId'})
export class MenuEntryPipe implements PipeTransform {
  transform(entries: MenuEntry[], categoryId: string): MenuEntry[] {
    return entries.filter(entry => entry.categoryId === categoryId);
  }
}