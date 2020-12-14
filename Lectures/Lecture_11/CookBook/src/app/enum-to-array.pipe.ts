import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'enumToArray' })
export class EnumToArrayPipe implements PipeTransform {
    transform(value: any): any {
        const items: any[] = [];

        // tslint:disable-next-line: forin
        for (const record in value) {
            const key = parseInt(record, 10);
            if (!Number.isNaN(key)) {
                items.push({ key, value: value[key] });
            }
        }

        return items;
    }
}
