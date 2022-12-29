import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterByDate'
})
export class FilterByDatePipe implements PipeTransform {
  transform(items: any[], date: Date): any[] {
    if (!items) {
      return [];
    }
    if (!date) {
      return items;
    }
    return items.filter(item => item.date.toDateString() === date.toDateString());
  }
}
