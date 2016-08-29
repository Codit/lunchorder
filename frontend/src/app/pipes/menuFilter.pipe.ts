import {Pipe, PipeTransform} from '@angular/core';
import { MenuEntry } from '../domain/dto/menuEntry';

@Pipe({
    name: 'menuFilter'
})

export class MenuFilterPipe implements PipeTransform {
       transform(entries: MenuEntry[], filterText: string): MenuEntry[] {
    return entries.filter(entry => entry.name.toLowerCase().indexOf(filterText.toLowerCase()) !== -1);
    }
}