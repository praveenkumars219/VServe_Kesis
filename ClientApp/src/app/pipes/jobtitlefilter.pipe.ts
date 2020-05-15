import { Pipe, PipeTransform, Injectable } from '@angular/core';
import { jobcode } from '../models/jobcode';

@Pipe({
  name: 'jobtitlefilter'
})

@Injectable()
export class JobtitlefilterPipe implements PipeTransform {

  transform(jobCodes: any[], args: any[]): any[] {
    console.log(jobCodes);
    const codes = jobCodes?.filter(x => x.JobTitle.includes(args[0]));
    console.log(codes);
    return codes;
  }

}
