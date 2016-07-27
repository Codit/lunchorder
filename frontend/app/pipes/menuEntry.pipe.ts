import { Pipe, PipeTransform } from '@angular/core';
import { MenuEntry } from '../domain/dto/menuEntry';
/*
 * Raise the value exponentially
 * Takes an exponent argument that defaults to 1.
 * Usage:
 *   value | exponentialStrength:exponent
 * Example:
 *   {{ 2 |  exponentialStrength:10}}
 *   formats to: 1024
*/
@Pipe({name: 'menuEntryByCategoryId'})
export class MenuEntryPipe implements PipeTransform {
  transform(entries: MenuEntry[], categoryId: string): MenuEntry[] {
      console.log("in the pipe!");
    return entries.filter(entry => entry.categoryId === categoryId);
  }
}
 