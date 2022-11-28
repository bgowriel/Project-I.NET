import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToasterComponent } from './toaster.component';

@Injectable()
export class ToasterService {
    constructor(private snackBar: MatSnackBar) { }

    private openSnackBar(message: string, type: string) {
        this.snackBar.openFromComponent(ToasterComponent, {
            duration: 3000,
            data: { message, type },
            horizontalPosition: "right",
            verticalPosition: "top",
        });
    }

    public onSuccess(message: string) {
        this.openSnackBar(message, "success")
    }

    public onError(message: string) {
        this.openSnackBar(message, "error")
    }
}
