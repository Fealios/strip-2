import { Component } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';



@Component({
    selector: 'nick',
    template: require('./nick.component.html'),
    styles: [require('./nick.component.css')]
})

export class NickTestComponent {

    constructor(private http: Http) { }

    strip(song: string) {
        this.http.get('/api/YouStrip/StripRequest/' + song)
            .subscribe(response => {
           this.redirect(response['_body'].slice(1, -1));
            //this.redirect(response['_body']);
        });
    }

    redirect(link) {
        window.location.href = link
    }

    fileChange(event) {
        console.log('in change');
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();
            formData.append('uploadFile', file, file.name);
            let headers = new Headers();
            headers.append('Accept', 'application/json');
            let options = new RequestOptions({ headers: headers });
            this.http.post('/api/YouStrip/Upload/', formData, options)
                .subscribe(response => {
                    console.log(response);
                });
        }
    }

}