import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
    imports: [
        MatCardModule,
        MatCheckboxModule,
        MatGridListModule,
        MatToolbarModule,
        MatSidenavModule,
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatListModule,
        MatMenuModule,
        MatIconModule,
        MatTableModule,
        MatDialogModule
    ],
    exports: [
        MatCardModule,
        MatCheckboxModule,
        MatGridListModule,
        MatToolbarModule,
        MatSidenavModule,
        MatFormFieldModule,
        MatButtonModule,
        MatInputModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatListModule,
        MatMenuModule,
        MatIconModule,
        MatTableModule,
        MatDialogModule
    ]
})
export class AppMaterialModule {
}
