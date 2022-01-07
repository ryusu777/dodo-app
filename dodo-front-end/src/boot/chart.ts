import { boot } from 'quasar/wrappers';
import { Chart, registerables } from 'chart.js';

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot((/* { app, router, ... } */) => {
  Chart.register(...registerables);
});
