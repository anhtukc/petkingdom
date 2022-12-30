import { Pipe, PipeTransform } from '@angular/core';
import { tableSchedule } from '../Class/Schdule';

@Pipe({
  name: 'filterByDate'
})
export class FilterByDatePipe implements PipeTransform {
  transform(items: tableSchedule[], date: Date): any[] {
    if (!items) {
      return [];
    }
    if (!date) {
      return items;
    }
    let result = items.filter(item => item.scheduleDate.toDateString() === date.toDateString())
    return result;
  }
}
