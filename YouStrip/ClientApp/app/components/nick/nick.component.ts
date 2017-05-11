import { Component } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'nick',
    template: require('./nick.component.html'),
    styles: [require('./nick.component.css')]
})

export class NickTestComponent {
    names: Array<string> = [];

    constructor(private http: Http) { }

    strip(song: string) {
        this.http.get('/api/YouStrip/StripRequest/' + song)
            .subscribe(response => {
           this.redirect(response['_body'].slice(1, -1));
            //this.redirect(response['_body']);
        });
    }

    redirect(link) {
        console.log(link);
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
                    //for (var i = 0; i < response['_body'].
                });
        }
    }

    getNames() {
        this.http.get('/api/YouStrip/GetFileNames')
            .subscribe(response => {
                this.names = response.json();
            });
    }

    playSong() {
        var audio = new Audio();
        audio.src = "Songs/Gorillaz - Feel Good Inc. (Official Video) (4).mp3";
        audio.load();
        audio.play();
        console.log(audio);
    }

    //getNames(): Observable<string[]> {
    //    console.log('in the click');
    //    return (this.http.get('/api/YouStrip/GetFileNames').map(this.extractData));
    //}

    //private extractData(res: Response) {
    //    let body = res.json();
    //    return body.data || {};
    //}


}