import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {
    

    transform(input: string): string {
        if (input)
            return input.split(' ').map(function (wrd) {
                return wrd.charAt(0).toUpperCase() + wrd.substr(1).toLowerCase();
            }).join(' ');
    }
}